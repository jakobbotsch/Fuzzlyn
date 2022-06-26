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

Fuzzlyn is actively used to test the .NET JIT compiler and runs automatically [every week in dotnet/runtime's CI](https://dnceng.visualstudio.com/public/_build?definitionId=1054&_a=summary).
For a more exhaustive list of issues see [this search](https://github.com/dotnet/runtime/issues?q=is%3Aissue+Fuzzlyn).