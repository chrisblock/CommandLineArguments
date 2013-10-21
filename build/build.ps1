$baseDir = Resolve-Path(".")
$solution = Join-Path $baseDir "source\CommandLineArguments.sln"
$windir = $env:windir

if((ls "$windir\Microsoft.NET\Framework\v4.0*") -eq $null ) {
	throw "Building requires .NET 4.0, which doesn't appear to be installed on this machine."
}

$v4_net_version = (ls "$windir\Microsoft.NET\Framework\v4.0*").Name

$msbuild = "$windir\Microsoft.NET\Framework\$v4_net_version\MSBuild.exe"

$options = "/m /noconsolelogger /p:Configuration=Release"

$clean = "$msbuild $options /t:Clean ""$solution"""

Invoke-Expression $clean

if ($LastExitCode -ne 0) {
	Write-Host "Error executing '$clean'." -ForegroundColor Red
	Exit 1
}

$build = "$msbuild $options /t:Build ""$solution"""

Invoke-Expression $build

if ($LastExitCode -ne 0) {
	Write-Host "Error executing '$build'." -ForegroundColor Red
	Exit 1
}

$nuget = Join-Path $baseDir "tools\NuGet\nuget.exe"

$project = Join-Path $baseDir "source\CommandLineArguments\CommandLineArguments.csproj"

$package = "$nuget pack $project -Prop Configuration=Release"

Invoke-Expression $package
