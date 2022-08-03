# Bugs

Here is a non-exhaustive list of bugs that were found by Fuzzlyn:

* ~~[NullReferenceException thrown for multi-dimensional arrays in release](https://github.com/dotnet/coreclr/issues/18232)~~
* ~~[Wrong integer promotion in release](https://github.com/dotnet/coreclr/issues/18235)~~
* ~~[Cast to ushort is dropped in release](https://github.com/dotnet/coreclr/issues/18238)~~
* ~~[Wrong value passed to generic interface method in release](https://github.com/dotnet/coreclr/issues/18259)~~
* [Constant-folding int.MinValue % -1](https://github.com/dotnet/roslyn/issues/27348)
* ~~[Deterministic program outputs indeterministic results on Linux in release](https://github.com/dotnet/coreclr/issues/18522)~~
* ~~[RyuJIT incorrectly reorders expression containing a CSE, resulting in exception thrown in release](https://github.com/dotnet/coreclr/issues/18770)~~
* ~~[RyuJIT incorrectly narrows value on ARM32/x86 in release](https://github.com/dotnet/coreclr/issues/18780)~~
* ~~[Invalid value numbering when morphing casts that changes signedness after global morph](https://github.com/dotnet/coreclr/issues/18850)~~
* ~~[RyuJIT spills 16 bit value but reloads as 32 bits in ARM32/x86 in release](https://github.com/dotnet/coreclr/issues/18867)~~
* ~~[RyuJIT fails to preserve variable allocated to RCX around shift on x64 in release](https://github.com/dotnet/coreclr/issues/18884)~~
* ~~[RyuJIT: Invalid ordering when assigning ref-return](https://github.com/dotnet/coreclr/issues/19243)~~
* ~~[RyuJIT: Argument written to stack too early on Linux](https://github.com/dotnet/coreclr/issues/19256)~~
* ~~[RyuJIT: Morph forgets about side effects when optimizing casted shift](https://github.com/dotnet/coreclr/issues/19272)~~
* ~~[RyuJIT: By-ref assignment with null leads to runtime crash](https://github.com/dotnet/coreclr/issues/19444)~~
* ~~[RyuJIT: Mishandling of subrange assertion for rewritten call parameter](https://github.com/dotnet/coreclr/issues/19558)~~
* ~~[RyuJIT: Incorrect ordering around Interlocked.Exchange and Interlocked.CompareExchange](https://github.com/dotnet/coreclr/issues/19583)~~
* ~~[RyuJIT: Missing zeroing of upper bits for small struct used in Volatile.Read](https://github.com/dotnet/coreclr/issues/19599)~~
* ~~[RyuJIT: Incorrect 4-byte immediate emitted for shift causes access violation](https://github.com/dotnet/coreclr/issues/19601)~~
* ~~[Finally block belonging to unexecuted try runs anyway](https://github.com/dotnet/roslyn/issues/29481)~~
* ~~[RyuJIT: Bad codegen with multiple field assignments](https://github.com/dotnet/runtime/issues/11559)~~
* ~~[Runtime crash during JIT register allocation in .NET 5](https://github.com/dotnet/runtime/issues/36237)~~
* ~~[[JIT] Runtime crash in fgMakeOutgoingStructArgCopy](https://github.com/dotnet/runtime/issues/36468)~~
* ~~[JIT EH write thru crash](https://github.com/dotnet/runtime/issues/54100)~~
* ~~[Invalid hoisting with refs/array elements proven to be constant](https://github.com/dotnet/runtime/issues/54118)~~
* ~~[JIT misses a zero extension in series of casts](https://github.com/dotnet/runtime/issues/55127)~~
* ~~[Invalid assertion prop in finally](https://github.com/dotnet/runtime/issues/55131)~~
* ~~[JIT incorrectly reorders method call and static field load](https://github.com/dotnet/runtime/issues/55140)~~
* ~~[Assertion failed 'src->IsCnsIntOrI()' with contained bitcast in field store](https://github.com/dotnet/runtime/issues/55141)~~
* ~~[JIT forgets to normalize arg on load](https://github.com/dotnet/runtime/issues/55143)~~
* ~~[JIT: flow opts does an incorrect pred list update](https://github.com/dotnet/runtime/issues/56495)~~
* ~~[ARM64 multiplication/anding by zero seems to be discarded](https://github.com/dotnet/runtime/issues/56930)~~
* ~~[ARM64 incorrect result when expression involves modulo by 1](https://github.com/dotnet/runtime/issues/56935)~~
* ~~[JIT performs invalid jump threading](https://github.com/dotnet/runtime/issues/56979)~~
* ~~[ARM32: runtime crash inside JIT](https://github.com/dotnet/runtime/issues/57061)~~
* ~~[ARM32: Incorrect split passing of promoted structs with padding](https://github.com/dotnet/runtime/issues/57064)~~
* ~~[ARM64: Incorrect sub expression reordering of tree containing CSE def](https://github.com/dotnet/runtime/issues/57121)~~
* ~~[JIT: Invalid negated result](https://github.com/dotnet/runtime/issues/57640)~~
* ~~[JIT: Assertion failed '!m_VariableLiveRanges->back().m_EndEmitLocation.Valid()'](https://github.com/dotnet/runtime/issues/57752)~~
* ~~[JIT: Assertion failed 'offset != BAD_STK_OFFS'](https://github.com/dotnet/runtime/issues/57767)~~
* ~~[JIT: Assertion failed 'm_compGenTreeID == m_compiler->compGenTreeID'](https://github.com/dotnet/runtime/issues/57775)~~
* ~~[ARM64: Assertion failed 'interval->isWriteThru'/runtime crash in release](https://github.com/dotnet/runtime/issues/58083)~~
* ~~[ARM32: Runtime crash with interfaces](https://github.com/dotnet/runtime/issues/58293)~~
* ~~[JIT still seems to be hoisting some modified indirs](https://github.com/dotnet/runtime/issues/58877)~~
* ~~[RyuJIT: Invalidly optimized negated division](https://github.com/dotnet/runtime/issues/60297)~~
* ~~[JIT: Discarded cast on RHS of assignment leads to missing sign extension](https://github.com/dotnet/runtime/issues/60597)~~
* ~~[JIT ARM32: Assertion failed 'size == EA_4BYTE' during 'Generate code'](https://github.com/dotnet/runtime/issues/60827)~~
* ~~[Assertion failed '!parentStruct->lvUndoneStructPromotion' during 'Mark local vars'](https://github.com/dotnet/runtime/issues/61074)~~
* ~~[JIT: Invalid optimization of (-1 * -A) + A](https://github.com/dotnet/runtime/issues/61077)~~
* ~~[ARM32 JIT: Assertion failed '!"NYI: odd sized struct in fgMorphMultiregStructArg"' during 'Morph - Global'](https://github.com/dotnet/runtime/issues/61168)~~
* ~~[JIT: Incorrectly optimized double-negation in face of inlining](https://github.com/dotnet/runtime/issues/61908)~~
* ~~[Assertion failed 'compiler->opts.IsOSR() || ((genInitStkLclCnt > 0) == hasUntrLcl)'](https://github.com/dotnet/runtime/issues/64808)~~
* ~~[Assertion failed '!"Write to unaliased local overlaps outstanding read"' during 'Rationalize IR'](https://github.com/dotnet/runtime/issues/64883)~~
* ~~[JIT: Incorrect results for programs in win-x86 release](https://github.com/dotnet/runtime/issues/64904)~~
* ~~[JIT: Incorrect result computed with forward sub](https://github.com/dotnet/runtime/issues/65104)~~
* ~~[Assertion failed '!"Write to unaliased local overlaps outstanding read"' during 'Lowering nodeinfo' (IL size 7) ](https://github.com/dotnet/runtime/issues/65307)~~
* [Assertion failed '(emitThisGCrefRegs & regMask) == 0' during 'Emit code' (IL size 129)](https://github.com/dotnet/runtime/issues/65311)
* ~~[JIT: Invalid result on x86 with forward sub](https://github.com/dotnet/runtime/issues/66242)~~
* ~~[JIT: Assertion failed 'type == genActualType(type)' during 'Morph - Global'](https://github.com/dotnet/runtime/issues/66269)~~
* ~~[JIT: Illegal reordering of field read and call on implicit byref](https://github.com/dotnet/runtime/issues/66414)~~
* [Assertion failed 'interval->isSpilled' during 'LSRA allocate'](https://github.com/dotnet/runtime/issues/66578)
* [Assertion failed 'inVarToRegMap[varIndex] == REG_STK' during 'LSRA allocate' ](https://github.com/dotnet/runtime/issues/66579)
* [Assertion failed '!foundMismatch' during 'LSRA allocate'](https://github.com/dotnet/runtime/issues/66580)
* ~~[ushort argument mysteriously mutated](https://github.com/dotnet/runtime/issues/66624)~~
* ~~[JIT: Incorrect optimization of x & -x on x86](https://github.com/dotnet/runtime/issues/66709)~~
* ~~[JIT: Writes to small args on macOS can (still) overwrite adjacent args](https://github.com/dotnet/runtime/issues/67331)~~
* ~~[JIT: Too narrow store for argument passed on stack on macOS ARM64](https://github.com/dotnet/runtime/issues/67344)~~
* ~~[Assertion failed 'lclNode->GetSsaNum() == SsaConfig::FIRST_SSA_NUM' during 'VN based copy prop'](https://github.com/dotnet/runtime/issues/67346)~~
* ~~[JIT: Wrong result computed with forward sub enabled on x64 Windows/Linux](https://github.com/dotnet/runtime/issues/68049)~~
* ~~[Assertion failed 'divMod->OperGet() != GT_UMOD' during 'Lowering nodeinfo'](https://github.com/dotnet/runtime/issues/68136)~~
* ~~[Assertion failed '!"Shouldn't see an integer typed GT_MOD node in ARM64"' during 'Linear scan register alloc'](https://github.com/dotnet/runtime/issues/68470)~~
* ~~[Assertion failed '!foundDiff' during 'Linear scan register alloc'](https://github.com/dotnet/runtime/issues/69659)~~
* ~~[JIT: Invalid results/assertion errors with modulo ops](https://github.com/dotnet/runtime/issues/70333)~~
* ~~[JIT: Assertion failed '(tree->AsCast()->gtCastType == m_tailcall->gtReturnType) && "Expected cast after tailcall to be no-op"'](https://github.com/dotnet/runtime/issues/70334)~~
* [x86: Assertion failed '!"Too many unreachable block removal loops"' during 'Global local var liveness'](https://github.com/dotnet/runtime/issues/70786)
* ~~[JIT: BBF_HAS_NULLCHECK is not set on BB but is required](https://github.com/dotnet/runtime/issues/71193)~~
* ~~[Assert failure: heapSize >= initialRequestSize with collectible assemblies on linux-x64](https://github.com/dotnet/runtime/issues/71200)~~
* [JIT ARM32: Assertion failed 'varDsc->lvRefCnt() == 0' during 'Generate code'](https://github.com/dotnet/runtime/issues/71543)
* ~~[Assertion failed 'i < BitSetTraits::GetSize(env)' in during 'Redundant branch opts'](https://github.com/dotnet/runtime/issues/71599)
* ~~[JIT: Invalid result computed with modulo operation](https://github.com/dotnet/runtime/issues/71600)~~

Fuzzlyn is actively used to test the .NET JIT compiler and runs automatically [every week in dotnet/runtime's CI](https://dnceng.visualstudio.com/public/_build?definitionId=1054&_a=summary).
For a more exhaustive list of issues see [this search](https://github.com/dotnet/runtime/issues?q=is%3Aissue+Fuzzlyn).
