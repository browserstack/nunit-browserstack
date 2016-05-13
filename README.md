# Nunit-Browserstack

Execute [NUnit](https://github.com/nunit/nunit) scripts on BrowserStack.

## Usage

### Prerequisites

Visual Studio 2015 update 2

### Clone the repo

`git clone https://github.com/browserstack/nunit-browserstack.git`

### Install dependencies

Open the appropriate Visual Studio Solution file (.sln) and run `build`.
Visual Studio will automatically download the dependencies

### BrowserStack Authentication

To run the tests, `BROWSERSTACK_USERNAME` and `BROWSERSTACK_ACCESS_KEY` needs to be replaced with BrowserStack authentication.
These can be found on the automate accounts page on [BrowserStack](https://www.browserstack.com/accounts/automate)

These needs to be changed in the following files -
```
nunit-browserstack/Parallel Tests/BrowserStackParallelTests/BrowserStackTests.cs
nunit-browserstack/Parallel Tests/BrowserStackParallelLocalTests/BrowserStackLocalTests.cs
nunit-browserstack/Series Tests/NUnit-BrowserStack/BrowserStackTests.cs
nunit-browserstack/Series Tests/NUnit-BrowserStack/BrowserStackLocalTests.cs
```

### Run the tests

 - Please refer to the README in the corresponding folders.

------

#### How to specify the capabilities

The [Code Generator](https://www.browserstack.com/automate/c-sharp#setting-os-and-browser) can come in very handy when specifying the capabilities especially for mobile devices.
