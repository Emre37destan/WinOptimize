## Run WinOptimize on Windows Server 2008-2012-2016-2019-2022 ##
#### Some options might not work properly ####
- ```WinOptimize.exe /unsafe```

## How to disable Windows Defender in Windows 10 1903 and later ##
#### https://docs.microsoft.com/en-us/windows-hardware/customize/desktop/unattend/security-malware-windows-defender-disableantispyware "DisableAntiSpyware" is discontinued and will be ignored on client devices, as of version 1903. ####

- Restart in SAFE MODE
- Execute: ```WinOptimize.exe /disabledefender```

-OR-

- Execute: ```WinOptimize.exe /restart=disabledefender``` and let WinOptimize do the rest automatically

## How to re-enable Windows Defender ##

- Restart in SAFE MODE
- Execute: ```WinOptimize.exe /enabledefender```

-OR-

- Execute: ```WinOptimize.exe /restart=enabledefender``` and let WinOptimize do the rest automatically

## How to restart in SAFE MODE / NORMAL easily ##

- ```WinOptimize.exe /restart=safemode```
- ```WinOptimize.exe /restart=normal```

## Display version info from command line using:

- ```WinOptimize.exe /version```

## You may disable specific tools for troubleshooting purposes ##
#### Available list: ####

* Hardware inspection utility (```indicium```)
* Common Apps downloader tool (```apps```)
* HOSTS Editor tool (```hosts```)
* UWP Apps Uninstaller (```uwp```)
* Startup items tool (```startup```)
* Cleaner utility (```cleaner```)
* Integrator tool (```integrator```)
* Pinger tool (```pinger```)

#### Examples ####

- ```WinOptimize.exe /disable=indicium,uwp```
- ```WinOptimize.exe /disable=indicium,uwp,hosts```

## Disable or Reset svchost process splitting mechanism ##
### Reduces the amount of svchost processes running, improving RAM usage ###
### To disable it, you need to provide your amount of RAM using this command (example for 8GB RAM): ###

```WinOptimize.exe /svchostsplit=8```

#### Reset the mechanism to its default configuration using: ####
```WinOptimize.exe /resetsvchostsplit```

## Reset WinOptimize configuration might fix it when can't open ##
```WinOptimize.exe /repair```

## How to disable/enable HPET (High Precision Event Timer) in order to gain a boost when gaming [use at your own risk!] ##

- ```WinOptimize.exe /disablehpet```
- ```WinOptimize.exe /enablehpet```
