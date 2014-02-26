https://gist.github.com/damianh/8639983
# Works for me. You may need to tweak it. Stick it in your powershell profile FTW
 
function Rename-Project
{
    # designed to run from the solution directory where the project is a child directory
    param(
        [string]$projectName=$(throw "projectName required."),
        [string]$newProjectName=$(throw "newProjectName required.")
    )
    
    $ErrorActionPreference = "Stop"
     
    if(!(Test-Path $projectName)){
        Write-Error "No project folder '$projectName' found"
        return
    }
     
    if(!(Test-Path $projectName\$projectName.csproj)){
        Write-Error "No project '$projectName\$projectName.dll' found"
        return
    }
     
    if((Test-Path $newProjectName)){
        Write-Error "Project '$newProjectName' already exists"
        return
    }
    "Renaming $projectName to $newProjectName..."
    
    # project
    rni "$projectName\$projectName.csproj" "$newProjectName.csproj"
     
    # folder
    rni $projectName $newProjectName
     
    # assembly title
    $assemblyInfoPath = "$newProjectName\Properties\AssemblyInfo.cs"
    (gc $assemblyInfoPath) -replace """$projectName""","""$newProjectName""" | sc $assemblyInfoPath
     
    # root namespace
    $projectFile = "$newProjectName\$newProjectName.csproj"
    (gc $projectFile) -replace "<RootNamespace>$projectName</RootNamespace>","<RootNamespace>$newProjectName</RootNamespace>" | sc $projectFile
     
    # assembly name
    (gc $projectFile) -replace "<AssemblyName>$projectName</AssemblyName>","<AssemblyName>$newProjectName</AssemblyName>" | sc $projectFile
     
    # other project references
    gci -Recurse -Include *.csproj |% { (gc $_) -replace "..\\$projectName\\$projectName.csproj", "..\$newProjectName\$newProjectName.csproj" | sc $_ }
    gci -Recurse -Include *.csproj |% { (gc $_) -replace "<Name>$projectName</Name>", "<Name>$newProjectName</Name>" | sc $_ }
     
    # solution 
    gci -Recurse -Include *.sln |% { (gc $_) -replace "\""$projectName\""", """$newProjectName""" | sc $_ }
    gci -Recurse -Include *.sln |% { (gc $_) -replace "\""$projectName\\$projectName.csproj\""", """$newProjectName\$newProjectName.csproj""" | sc $_ }
}