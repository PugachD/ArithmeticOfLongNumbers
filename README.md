# ArithmeticOfLongNumbers
В корне каталога с проектом есть тестовые файлы, на которых я проверял выполнение программы (1.txt, 2.txt, 3.txt). 
Время выполнения считается с помощью таймера DispetcherTimer с некоторым интервалом. Чем меньше интервал 
(ближе к 1 мс), тем правильнее и чаще будет считаться процент данной операции от общего времени выполнения. 
Но это время не будет соответствовать естесственному времени. Если выставлять интервал больше (в районе 100-500 мс),
то будет видна погрешность во времени, но четче будет прогноз. На маленьких файлах (при коротком интервале) 
мы увидим, что время работы равно 0 и процент данной операции от общего времени выполнения тоже равен 0,
 так как вычисления могут выполниться за время меньшее, чем интервал и данные не успеют обновиться.
 Интервал private TimeSpan timerInterval равен 222 мс. Находится он в файле BasicCalculations.
 В программе многопоточность выполняется с помощью операции Parallel.Foreach, которая находится в фоновом 
 от основного потока с помощью BackgroundWorker. Из-за того же BackgroundWorker отмена операции происходит не сразу,
  так как программа ждет завершения незаконченных потоков и сообщает всем оставшимся о завершении.
 
