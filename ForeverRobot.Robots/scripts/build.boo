import PhantomContrib

slnFile = "ForeverRobot.Users.sln"
iisSiteName = "ForeverRobot.User"
mSpecRunner = "packages\\Machine.Specifications.0.5.6.0\\tools\\mspec-clr4.exe"
pathToSpecification = "Source\\ForeverRobot.Users.Specification\\bin\\Release\\ForeverRobot.Users.Specification.dll"

target default(compile, runSpecifications, setupIIS):
  pass
  
target compile:
  msbuild(file: slnFile, configuration: "release")

target runSpecifications:
  exec(mSpecRunner, pathToSpecification)

target setupIIS:
  if iis_exists(iisSiteName):
    iis7_remove_site(iisSiteName)

  iis7_create_site(iisSiteName, "http","*:19100:user.foreverrobot.com","Source\\ForeverRobot.Users\\")
 

