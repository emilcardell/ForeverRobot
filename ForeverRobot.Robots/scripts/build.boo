import PhantomContrib
import System.Security.AccessControl

slnFile = "ForeverRobot.Robots.sln"
iisSiteName = "ForeverRobot.Robot"
mSpecRunner = "packages\\Machine.Specifications.0.5.6.0\\tools\\mspec-clr4.exe"
pathToSpecification = "Source\\ForeverRobot.Robots.Specification\\bin\\Release\\ForeverRobot.Robots.Specification.dll"
iisPath = "Source\\ForeverRobot.Robots\\"

target default, (compile, runSpecifications, setupIIS):
  pass
  
target compile:
  msbuild(file: slnFile, configuration: "release")

target runSpecifications:
  exec(mSpecRunner, pathToSpecification)

target setupIIS:
  SetDirectoryPermission(iisPath, "IIS_IUSRS", Phantom.Core.Builtins.PermissionLevel.Full)
  iis7_remove_site(iisSiteName)
  iis7_create_site(siteName: iisSiteName, path: iisPath, bindingInformation: "*:80:robot.foreverrobot.com", bindingProtocol: "http" ) 
    
     
  
  
 

