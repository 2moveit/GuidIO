# GuidIO
GuidIO manages the directory structure for files.

```csharp
public void Demo()
{
	var id1 = new Guid("11111111-24C9-4427-BE39-4FEFD92DD569");
	var id2 = new Guid("11112222-24C9-4427-BE39-4FEFD92DD569");
	var id3 = new Guid("11223344-24C9-4427-BE39-4FEFD92DD569");
	var id4 = new Guid("12345678-24C9-4427-BE39-4FEFD92DD569");
	const string id5 = "abc.txt";
	
	var managedDirs = new ManagedDirs();
	managedDirs.Create(id1, "./Demo_2_2", "txt");   // ./Demo_2_2/11/11/11111111-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id2, "./Demo_2_2", "txt");   // ./Demo_2_2/11/11/11112222-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id3, "./Demo_2_2", "txt");   // ./Demo_2_2/11/22/11223344-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id4, "./Demo_2_2", "txt");   // ./Demo_2_2/12/34/12345678-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id5, "./Demo_2_2");          // ./Demo_2_2/ab/c/abc.txt

	managedDirs = new ManagedDirs(1);
	managedDirs.Create(id1, "./Demo_1_2", "txt");   // ./Demo_1_2/1/1/11111111-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id2, "./Demo_1_2", "txt");   // ./Demo_1_2/1/1/11112222-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id3, "./Demo_1_2", "txt");   // ./Demo_1_2/1/2/11223344-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id4, "./Demo_1_2", "txt");   // ./Demo_1_2/1/2/12345678-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id5, "./Demo_1_2");          // ./Demo_1_2/a/b/abc.txt

	managedDirs = new ManagedDirs(2, 1);
	managedDirs.Create(id1, "./Demo_2_1", "txt");    // ./Demo_2_1/11/11111111-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id2, "./Demo_2_1", "txt");    // ./Demo_2_1/11/11112222-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id3, "./Demo_2_1", "txt");    // ./Demo_2_1/11/11223344-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id4, "./Demo_2_1", "txt");    // ./Demo_2_1/12/12345678-24C9-4427-BE39-4FEFD92DD569.txt
	managedDirs.Create(id5, "./Demo_2_1");           // ./Demo_2_1/ab/abc.txt
}
```