import PhantomContrib
import System.Security.AccessControl

slnFile = "ForeverRobot.RobotCommands.sln"
iisSiteName = "ForeverRobot.Position"
mSpecRunner = "packages\\Machine.Specifications.0.5.6.0\\tools\\mspec-clr4.exe"
pathToSpecification = "Source\\ForeverRobot.RobotCommands.Specification\\bin\\Release\\ForeverRobot.RobotCommands.Specification.dll"
iisPath = "Source\\ForeverRobot.RobotCommands\\"

target default, (compile, runSpecifications, setupIIS):
  pass
  
target compile:
  msbuild(file: slnFile, configuration: "release")

target runSpecifications:
  exec(mSpecRunner, pathToSpecification)

target setupIIS:
  SetDirectoryPermission(iisPath, "IIS_IUSRS", Phantom.Core.Builtins.PermissionLevel.Full)
  iis7_remove_site(iisSiteName)
  iis7_create_site(siteName: iisSiteName, path: iisPath, bindingInformation: "*:80:position.foreverrobot.com", bindingProtocol: "http" ) 
    
     
  
  
 

