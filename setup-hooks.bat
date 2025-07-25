@echo off
echo Setting up Git hooks...

REM Configure Git to use custom hooks directory
git config core.hooksPath .githooks

REM Make the hook executable (for Git Bash/WSL compatibility)
if exist ".githooks\pre-commit" (
    git update-index --chmod=+x .githooks/pre-commit
)

echo Git hooks setup completed!
echo.
echo The pre-commit hook will now automatically format staged C# files before each commit.
echo.
echo To test the hook, try making a change to a C# file and commit it.
pause