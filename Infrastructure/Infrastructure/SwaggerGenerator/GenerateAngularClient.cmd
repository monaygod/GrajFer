@echo off


npx openapi-generator-cli generate -g typescript-angular   -i %2 -o ..\..\..\..\..\AngularFrontEnd\hawk\src\app\hawk-cube\%1    --additional-properties=apiModulePrefix=%3
																									