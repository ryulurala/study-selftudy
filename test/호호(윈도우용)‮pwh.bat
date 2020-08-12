@echo off
title 메롱
color 0F
echo "지금부터 시간 셉니다. 10초 후에 절전모드 갑니당~"
timeout 10 /nobreak
%systemroot%\system32\scrnsave.scr /s
cls
echo "뻥이지롱~ 걍 모니터만 끈거다"
echo.
echo "근데 여기서 진짜 아무 키나 더 누르면 진짜 절전모드"
echo.
color 0C
echo "Ctrl+C 누르거나 종료해야댐"
pause>nul
cls
color 0E
echo "또 뻥이다! -저작자 최성률무차"
pause>nul