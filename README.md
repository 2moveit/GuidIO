# GuidIO
[![Join the chat at https://gitter.im/2moveit/GuidIO](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/2moveit/GuidIO?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge) [![NuGet Status](http://img.shields.io/nuget/v/GuidIO.svg?style=flat)](https://www.nuget.org/packages/GuidIO/) [![AppVeyor build status](https://ci.appveyor.com/api/projects/status/github/2moveit/GuidIO?branch=master&svg=true)](https://ci.appveyor.com/project/2moveit/guidio) [![Coverity Scan](https://img.shields.io/coverity/scan/5059.svg)](https://scan.coverity.com/projects/5059) [![MIT license](http://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/2moveit/GuidIO/blob/master/LICENSE)

### Install
To install GuidIO via [NuGet](https://www.nuget.org/packages/GuidIO/), run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

**<code>
PM&gt; Install-Package GuidIO
</code>**


### Usage

```csharp
    public void Demo()
    {
	var id1 = new Guid("11111111-24C9-4427-BE39-4FEFD92DD569");
	var id2 = new Guid("11112222-24C9-4427-BE39-4FEFD92DD569");
	var id3 = new Guid("11223344-24C9-4427-BE39-4FEFD92DD569");
	var id4 = new Guid("12345678-24C9-4427-BE39-4FEFD92DD569");
	const string id5 = "abc.txt";
	
	// Default configuration: 2 characters as directory size; subdirectory depth 2
	var managedDirs = new ManagedDirs();
	managedDirs.Create(id1, "./Demo_2_2", "txt");       // .\Demo_2_2\11\11\11111111-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id2, "./Demo_2_2", "txt");       // .\Demo_2_2\11\11\11112222-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id3, "./Demo_2_2", "txt");       // .\Demo_2_2\11\22\11223344-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id4, "./Demo_2_2", "txt");       // .\Demo_2_2\12\34\12345678-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id5, "./Demo_2_2");               // .\Demo_2_2\ab\c\abc.txt
	
	// 1 character as directory size; subdirectory depth 2 
	managedDirs = new ManagedDirs(1);
	managedDirs.Create(id1, "./Demo_1_2", "txt");       // .\Demo_1_2\1\1\11111111-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id2, "./Demo_1_2", "txt");       // .\Demo_1_2\1\1\11112222-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id3, "./Demo_1_2", "txt");       // .\Demo_1_2\1\2\11223344-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id4, "./Demo_1_2", "txt");       // .\Demo_1_2\1\2\12345678-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id5, "./Demo_1_2");               // .\Demo_1_2\a\b\abc.txt
	
	// 2 character as directory size; subdirectory depth 1 
	managedDirs = new ManagedDirs(2, 1);
	managedDirs.Create(id1, "./Demo_2_1", "txt");       // .\Demo_2_1\11\11111111-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id2, "./Demo_2_1", "txt");       // .\Demo_2_1\11\11112222-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id3, "./Demo_2_1", "txt");       // .\Demo_2_1\11\11223344-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id4, "./Demo_2_1", "txt");       // .\Demo_2_1\12\12345678-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id5, "./Demo_2_1");               // .\Demo_2_1\ab\abc.txt

        // Default configuration with ignore filter for characters
        managedDirs = new ManagedDirs(ignoreChar: new[] {'-'});
        managedDirs.Create("1-2_3.txt", "./Demo_ignore");                      // .\Demo_ignore\12\_3\1-2_3.txt

	// Get directory path
        string dir1 = managedDirs.GetDirPath("1-2_3.txt");                     // .\12\_3
        string dir2 = managedDirs.GetDirPath("1-2_3.txt", "./Demo_ignore");    // ./Demo_ignore\12\_3

        // Write "Demo text content" into the file
        managedDirs.WriteAllText(id1, "Demo text content", "./Demo_WriteAllText", "txt");                 // .\Demo_WriteAllText\11\11\11111111-24C9-4427-BE39-4FEFD92DD569.txt
        managedDirs.WriteAllText("1-2_3WriteAllText.txt", "Demo text content", "./Demo_WriteAllText");    // .\Demo_WriteAllText\12\_3\1-2_WriteAllText.txt

        // Read text from file
        string content1 = managedDirs.ReadAllText(id1, "./Demo_WriteAllText", "txt");                    // Demo text content
        string content2 = managedDirs.ReadAllText("1-2_3WriteAllText.txt", "./Demo_WriteAllText");       // Demo text content 
    }
```

Comments, ideas, suggestions welcome.
