<img src="https://raw.githubusercontent.com/bchavez/Havoc/master/Docs/logo.png" align='right' />

Havoc
======================

Project Description
-------------------
Hello. I'm your host **[Brian Chavez](https://github.com/bchavez)** ([twitter](https://twitter.com/bchavez)). **Havoc** is a collection of dangerous code that can wreck havoc in .NET applications and the operating system. **Havoc** is built on [**Bogus**][1] generator fake data generator for .NET. 

**Havoc** will help you stress test your .NET applications under various load, simulation, and failure-injection scenarios. If you like **Havoc** star :star: the repository and show your friends! :smile: :dizzy: :muscle: 


### Download & Install
**Nuget Package [Havoc](https://www.nuget.org/packages/Havoc/)**

```powershell
Install-Package Havoc
```
Minimum Requirements: **.NET Standard 1.3** or **.NET Standard 2.0** or **.NET Framework 4.0**.

Usage
-----
### Havoc Scenarios

* **`Cpu`** 
	* `Stress` - Saturate the CPU with excessive computational work.
	* `ContextSwitching` - Create a high amount of thread context switching.
* **`Program`**
  	* `Deadlock` - Create a massive mount of deadlocked threads.
  	* `Threads` - Create a massive amount of threads in a process.
  	* `FileOpenHandles` - Excessively create large amount of open file handles.
  	* `ThreadPoolStarvation` - Create a situation where the thead pool is starved.
	* `ThreadPoolChaos` - Keeps reference to thread pool threads, then later calls `Abort()` randomly causing thread pool threads to randomly abort executing code.
* **`Disk`**
 	* `WriteEicar` - Write an [EICAR][2] test string to disk that will cause an anti-virus scanners to trigger.
	* `CachedWrites` - Write as fast as possible using the same data to disk.
	* `CachedReads` - Read as fast as possible reading the same data on disk. 
	* `RandomWrites` - Write random data to disk as fast as possible.
	* `RandomReads` - Read random data on disk as fast as possible.
	* `RandomIO` - Random reads and writes as fast as possible.
	* `CachedIO` - Cached reads and writes as fast as possible. 
* **`Memory`**
  	* `MemoryLeak` - Create a slow memory leak situation.
  	* `OutOfMemory` - Excessive memory allocation that causes `OutOfMemoryException`.
  	* `StackOverflow` - Generate a stack overflow exception.
  	* `ExcessiveGC` - Create a work load that causes excessive amounts of GC pauses.
  	* `MemoryCorruption` - Reflects into object and manipulates private values.
    * `DotNetFrameworkCorruption` - Static values in the **.NET Framework** are manipulated at runtime with various out of range values that can cause problems if calling code is invoked.
* **`Network`**
    * `TcpPortExhaustion` - Exhaust the number of available TCP/IP ports on the local operating system.
    * `TcpSend` - Sends TCP data as fast as possible saturating a TCP link with random data.
    * `UdpSend` - Send UDP data as fast as possible saturating UDP packets with random data. 
* **`Os`**
    * `WaitHandles` - Create a massive amount of wait handles registered in the operating system.
    * `Mutex` - Register a massive amount of handles in the operating system.
    * `SystemTimers` - Create massive amounts of System.Timers that fire at random times.
    * `Processes` - Create a massive amount of processes in the operating system.
* **`Windows`**
    * `BlueScreen` - Cause a blue screen kernel bug check.
* **`Dangerous`**
	* `DiskCorruption` - Randomly modify executing assembly and reference assemblies.
	* `DiskFull` - Continuously fill the disk until there is no free space left.




[1]:https://www.nuget.org/packages/Bogus/
[2]:https://en.wikipedia.org/wiki/EICAR_test_file