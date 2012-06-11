import PhantomContrib

slnFile = "ForeverRobot.Robots.sln"
iisSiteName = "ForeverRobot.Robot"
mSpecRunner = "packages\\Machine.Specifications.0.5.6.0\\tools\\mspec-clr4.exe"
pathToSpecification = "Source\\ForeverRobot.Robots.Specification\\bin\\Release\\ForeverRobot.Robots.Specification.dll"

target default(compile, runSpecifications, setupIIS):
  pass
  
target compile:
  msbuild(file: slnFile, configuration: "release")

target runSpecifications:
  exec(mSpecRunner, pathToSpecification)

target setupIIS:
  if iis_exists(iisSiteName):
    iis7_remove_site(iisSiteName)

  iis7_create_site(iisSiteName, "http","*:80:robot.foreverrobot.com","Source\\ForeverRobot.Robots\\")
 

