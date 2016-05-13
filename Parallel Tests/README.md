# Nunit-Browserstack

Execute [NUnit](https://github.com/nunit/nunit) scripts on BrowserStack.

### Run Parallel tests

### Run the tests

NUnit 3 provides a `nunit3-console.exe` command line utility to run the tests in parallel.
This utility is downloaded via `NUnit.Runners` dependency when the solution is build by Visual Studio.

To run the tests, first build the solution file in Visual Studio,
then change the working directory to the solution directory ie. `NUnit-BrowserStack\Parallel Tests`
You can run the tests via `nunit3-console.exe <path-to-project-assembly-1> <path-to-project-assembly-2> --workers=<number-of-workers-in-parallel>`

example -
```
C:\Users\Admin\Documents\Visual Studio 2015\Projects\NUnit-BrowserStack\Parallel Tests>packages\NUnit.ConsoleRunner.3.2.1\tools\nunit3-console.exe BrowserStackParallelTests\bin\Debug\BrowserStackParallelTests.dll BrowserStackParallelLocalTests\bin\Debug\BrowserStackParallelLocalTests.dll --workers=3
```
