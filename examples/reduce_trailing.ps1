Get-Content -Path ..\Fuzzlyn\bin\Release\netcoreapp2.1\publish\Execution_Mismatch.txt -Wait | % {
	if ($_ -notmatch '^Seed: [0-9]+$') {
	    return
	}

	$seed = ($_ -split ' ')[1]
	if (Test-Path "reduced\\$seed.cs") {
		Write-Host "Skipping $seed because it is already reduced"
		return
	}
	
	Write-Host "Reducing $seed"
	corerun ..\Fuzzlyn\bin\Release\netcoreapp2.1\publish\Fuzzlyn.dll --seed=$seed --reduce > "reduced\\$seed.cs"
	Write-Host "  ..done"
}