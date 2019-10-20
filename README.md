[1]:https://www.nuget.org/packages/Bogus/

Bogus.Havoc
======================

Project Description
-------------------
Hello. I'm your host **[Brian Chavez](https://github.com/bchavez)** ([twitter](https://twitter.com/bchavez)). **Bogus.Havoc** is a collection of dangerous code that can wreck havoc in .NET applications and the operating system. **Bogus.Havoc** is built on [**Bogus**][1] generator fake data generator for .NET. 

**Bogus.Havoc** will help you stress test your .NET applications under various load and failure scenarios. If you like **Bogus.Havoc** star :star: the repository and show your friends! :smile: If you find **Bogus.Havoc** useful consider supporting the [**Bogus**][1] project! :dizzy: :muscle: 


### Download & Install
**Nuget Package [Bogus.Havoc](https://www.nuget.org/packages/Bogus.Havoc/)**

```powershell
Install-Package Bogus.Havoc
```
Minimum Requirements: **.NET Standard 1.3** or **.NET Standard 2.0** or **.NET Framework 4.0**.

Usage
-----
### Bogus.Havoc Scenarios

* **`Cpu`** 
	* `Stress` - Saturate the CPU with excessive computational work.
* **`Program`**
  	* `Deadlock` - Create a massive mount of deadlocked threads.
  	* `Threads` - Create a massive amount of threads in a process.
  	* `ThreadPoolStarvation` - Create a situation where the thead pool is starved.
  	* `MemoryLeak` - Create a slow memory leak situation.
  	* `OutOfMemory` - Excessive memory allocation that causes `OutOfMemoryException`.   
  	* `ExcessiveGC` - Create a work load that causes excessive amounts of GC pauses.
	* `FileOpenHandles` - Excessively create large amount of open file handles.
* **`Disk`**
	* `CachedWrites` - Write as fast as possible using the same data to disk.
	* `CachedReads` - Read as fast as possible reading the same data on disk. 
	* `RandomWrites` - Write random data to disk as fast as possible.
	* `RandomReads` - Read random data on disk as fast as possible.
	* `RandomIO` - Random reads and writes as fast as possible.
	* `CachedIO` - Cached reads and writes as fast as possible.
* **`Network`**
    * `TcpSend` - Sends TCP data as fast as possible saturating a TCP link with random data.
    * `UdpSend` - Send UDP data as fast as possible saturating UDP packets with random data.
* **`Os`**
    * `WaitHandles` - Create a massive amount of wait handles registered in the operating system.
    * `Mutex` - Register a massive amount of handles in the operating system.
    * `SystemTimers` - Create massive amounts of System.Timers that fire at random times.
* **`Windows`**
    * `BlueScreen` - Cause a blue screen kernel bug check.