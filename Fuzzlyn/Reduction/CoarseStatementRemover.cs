using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzlyn.Reduction
{
    internal class CoarseStatementRemover : CSharpSyntaxRewriter
    {
        public CoarseStatementRemover(Func<MethodDeclarationSyntax, MethodDeclarationSyntax, bool> isInteresting)
        {
            IsInteresting = isInteresting;
        }

        public Func<MethodDeclarationSyntax, MethodDeclarationSyntax, bool> IsInteresting { get; }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax orig)
        {
            // Annotate all nodes
            var annotated = (MethodDeclarationSyntax)new IdAnnotator().Visit(orig);

            // Try removing all first
            MethodDeclarationSyntax withAllRemoved = Remove(annotated, GetRemovableNodes(annotated));
            if (IsInteresting(orig, withAllRemoved))
                return withAllRemoved;

            MethodDeclarationSyntax candidate = BinarySearch(orig, annotated, true);
            candidate = BinarySearch(orig, candidate, false);

            return candidate;
        }

        private MethodDeclarationSyntax Remove(MethodDeclarationSyntax method, IEnumerable<SyntaxNode> nodes)
        {
            IEnumerable<string> ids = nodes.Select(n => n.GetAnnotations("id").Single().Data);
            return (MethodDeclarationSyntax)new RemoveIdsRewriter(new HashSet<string>(ids)).Visit(method);
        }

        private MethodDeclarationSyntax BinarySearch(MethodDeclarationSyntax orig, MethodDeclarationSyntax current, bool forEnd)
        {
            List<StatementSyntax> removableNodes = GetRemovableNodes(current);

            int min = 0;
            int max = removableNodes.Count;

            MethodDeclarationSyntax best = current;
            while (min + 1 < max)
            {
                int mid = min + (max - min) / 2;

                int start = forEnd ? mid : 0;
                int end = forEnd ? removableNodes.Count : mid;

                MethodDeclarationSyntax newCandidate =
                    Remove(current, removableNodes.Skip(start).Take(end - start));

                if (IsInteresting(orig, newCandidate))
                {
                    best = newCandidate;

                    if (forEnd)
                        max = mid;
                    else
                        min = mid;
                }
                else
                {
                    if (forEnd)
                        min = mid;
                    else
                        max = mid;
                }
            }

            return best;
        }

        private List<StatementSyntax> GetRemovableNodes(MethodDeclarationSyntax method)
        {
            return
                method
                .DescendantNodes()
                .OfType<StatementSyntax>()
                .Where(s => !(s is LocalDeclarationStatementSyntax || s is ReturnStatementSyntax) && s.Parent is BlockSyntax)
                .ToList();
        }

        private class IdAnnotator : CSharpSyntaxRewriter
        {
            private int _id;
            public override SyntaxNode Visit(SyntaxNode node)
            {
                node = base.Visit(node);
                if (node == null)
                    return null;

                return node.WithAdditionalAnnotations(new SyntaxAnnotation("id", (_id++).ToString()));
            }
        }

        private class RemoveIdsRewriter : CSharpSyntaxRewriter
        {
            private readonly HashSet<string> _ids;
            public RemoveIdsRewriter(HashSet<string> ids)
            {
                _ids = ids;
            }

            public override SyntaxNode Visit(SyntaxNode node)
            {
                string id = node?.GetAnnotations("id")?.FirstOrDefault()?.Data;
                if (id != null && _ids.Contains(id))
                    return null;

                return base.Visit(node);
            }
        }
    }
}
