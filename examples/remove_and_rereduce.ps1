Remove-Item Rereduce_required.txt -ErrorAction Ignore
corerun ..\Fuzzlyn\bin\Release\netcoreapp2.1\publish\Fuzzlyn.dll --remove-fixed=reduced

$seeds = Get-Content Rereduce_required.txt | sort

workflow Invoke-Reduce {
    Param($seeds)
    
	ForEach -parallel -throttlelimit 4 ($seed in $seeds)
	{
		Start-Process -WindowStyle hidden -Wait "cmd.exe" "/c corerun ..\Fuzzlyn\bin\Release\netcoreapp2.1\publish\Fuzzlyn.dll --seed $seed --reduce > reduced\$seed.cs"
	}
}

Invoke-Reduce -seeds $seeds