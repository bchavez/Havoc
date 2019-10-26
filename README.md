[![Downloads](https://img.shields.io/nuget/dt/Havoc.svg)](https://www.nuget.org/packages/Havoc/) [![Build status](https://ci.appveyor.com/api/projects/status/6eaham1c9eb2fokk/branch/master?svg=true)](https://ci.appveyor.com/project/bchavez/havoc/branch/master)  [![Twitter](https://img.shields.io/twitter/url/https/github.com/bchavez/Havoc.svg?style=social)](https://twitter.com/intent/tweet?text=Havoc%20chaos-engineering%20for%20.NET:&amp;amp;url=https%3A%2F%2Fgithub.com%2Fbchavez%2FHavoc) <img src="https://raw.githubusercontent.com/bchavez/Havoc/master/Docs/logo.png" align='right' />

Havoc
======================

Project Description
-------------------
Hello. I'm your host **[Brian Chavez](https://github.com/bchavez)** ([twitter](https://twitter.com/bchavez)). **Havoc** is a collection of dangerous code that wreck havoc in .NET applications and the operating system for chaos-engineering. **Havoc** is built on [**Bogus**][1] generator fake data generator for .NET. 

**Havoc** can help you stress test your .NET applications under various load conditions, simulation, fault and failure-injection scenarios. If you like **Havoc** star :star: the repository and show your friends! :smile: :dizzy: :muscle: 


### Download & Install
**Nuget Package [Havoc](https://www.nuget.org/packages/Havoc/)**

```powershell
Install-Package Havoc
```
Minimum Requirements: **.NET Standard 2.0** or **.NET Framework 4.0**.

Usage
-----
### Havoc Scenarios
Legend: :x: - Not Implemented Yet.

* **`Cpu`** 
	* `Stress` - Saturate the CPU with excessive computational work.
	* :x: `ContextSwitching` - Create a high amount of thread context switching.
* **`Process`**
  	* :x: `MassDeadlock` - Create a massive mount of deadlocked threads.
  	* `MassThread` - Create a massive amount of threads in a process.
  	* `FileOpenHandles` - Excessively create large amount of open file handles.
  	* `ThreadPoolStarvation` - Create a situation where the thead pool is starved.
	* `ThreadPoolChaos` - Keeps reference to thread pool threads, then later calls `Abort()` randomly causing thread pool threads to randomly abort executing code.
	* `ProcessExit` - Calls `Environment.FailFast` and terminates the current running process immediately.
* **`Disk`**
	* :x: `CachedWrites` - Write as fast as possible using the same data to disk.
	* :x: `CachedReads` - Read as fast as possible reading the same data on disk. 
	* :x: `RandomWrites` - Write random data to disk as fast as possible.
	* :x: `RandomReads` - Read random data on disk as fast as possible.
	* :x: `RandomIO` - Random reads and writes as fast as possible.
	* :x: `CachedIO` - Cached reads and writes as fast as possible. 
* **`Memory`**
  	* `MemoryLeak` - Create a slow memory leak situation.
  	* `OutOfMemory` - Excessive memory allocation that causes `OutOfMemoryException`.
  	* `StackOverflow` - Generate a stack overflow exception.
  	* :x: `ExcessiveGC` - Create a work load that causes excessive amounts of GC pauses.
  	* :x: `MemoryCorruption` - Reflects into object and manipulates private values.
    * :x: `DotNetFrameworkCorruption` - Static values in the **.NET Framework** are manipulated at runtime with various out of range values that can cause problems if calling code is invoked.
* **`Network`**
    * `LocalTcpPortExhaustionAsync` - Exhaust the number of available TCP/IP ports on the local operating system.
    * :x: `TcpConnectionExhaustion` - Create a massive amount of TCP/IP connections to a given host.
    * :x: `TcpSend` - Sends TCP data as fast as possible saturating a TCP link with random data.
    * :x: `UdpSend` - Send UDP data as fast as possible saturating UDP packets with random data. 
* **`Os`**
    * :x: `WaitHandles` - Create a massive amount of wait handles registered in the operating system.
    * `MassMutex` - Register a massive amount of handles in the operating system.
    * `MassSystemTimer` - Create massive amounts of `System.Timers.Timer` that fire at random times.
    * :x: `Processes` - Create a massive amount of processes in the operating system.
* **`Windows`**
    * :x: `BlueScreen` - Cause a blue screen kernel bug check.
* **`Dangerous`**
	* :x: `DiskCorruption` - Randomly modify executing assembly and reference assemblies.
	* `DiskFull` - Continuously fill the disk until there is no free space left.
	* `WriteEicar` - Write an [EICAR][2] test string to disk that will cause an anti-virus scanners to trigger.
 	* `WriteEicarMany` - Write an anti-virus test string to a folder on disk, creating as many Eicar files as possible. The directory will be filled with random file names and their contents with the Eicar test value.




[1]:https://www.nuget.org/packages/Bogus/
[2]:https://en.wikipedia.org/wiki/EICAR_test_file