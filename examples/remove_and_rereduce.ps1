Remove-Item Rereduce_required.txt -ErrorAction Ignore
& ..\Fuzzlyn\publish\windows-x64\Fuzzlyn.exe --remove-fixed=reduced

$seeds = Get-Content Rereduce_required.txt | sort

workflow Invoke-Reduce {
    Param($seeds)
    
	ForEach -parallel -throttlelimit 4 ($seed in $seeds)
	{
		Start-Process -WindowStyle hidden -Wait "cmd.exe" "/c ..\Fuzzlyn\publish\windows-x64\Fuzzlyn.exe --seed $seed --reduce > reduced\$seed.cs"
	}
}

Invoke-Reduce -seeds $seeds