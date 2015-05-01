// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile

RestorePackages()

// Directories
let buildDir  = @".\build\"
let testDir   = @".\tests\"
let packagesDir = @".\packages"

// tools
let fxCopRoot = @".\Tools\FxCop\FxCopCmd.exe"

// version info
let version = "0.1"

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
)

Target "SetVersion" (fun _ ->
    CreateCSharpAssemblyInfo "./GuidIO/Properties/AssemblyInfo.cs"
        [Attribute.Title "GuidIO"
         Attribute.Description "GuidIO manages the directory structure for files automatically based on the file name."
         Attribute.Company "Kai Timmermann"
         Attribute.Copyright "Copyright Kai Timmermann © 2015"
         Attribute.Guid "e20d9eef-3857-4661-b5ea-34f8bede1541"
         Attribute.Product "GuidIO"
         Attribute.Version version
         Attribute.FileVersion version]
)

Target "CompileApp" (fun _ ->
    !! @"GuidIO\**\*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "CompileTests" (fun _ ->
    !! @"GuidIO.tests\**\*.csproj"
      |> MSBuildDebug testDir "Build"
      |> Log "TestBuild-Output: "
)


// Dependencies
"Clean"
  ==> "SetVersion"
  ==> "CompileApp"
  ==> "CompileTests"
  
// start build
RunTargetOrDefault "CompileTests"