@echo off

npx openapi-generator-cli generate -g typescript-angular   -i %2 -o .\%1    --additional-properties=apiModulePrefix=%3
																									