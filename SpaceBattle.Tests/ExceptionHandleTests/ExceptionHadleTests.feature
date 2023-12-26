Функция: Обработчка ошибок

    Сценарий: Обработка известной дереву ошибки, вызванной известной дереву командой
        Дано во время выполнения команды возникло исключение 
        Когда исключение типа SetPosEx, вызванное командой типа MoveCommand, обрабатывается
        Тогда срабатывает первый обработчик

    Сценарий: Обработка неизвестной дереву ошибки, вызванной известной дереву командой
        Дано во время выполнения команды возникло исключение
        Когда исключение неизвестного типа, вызванное командой типа MoveCommand, обрабатывается
        Тогда срабатывает второй обработчик

    Сценарий: Обработка известной дереву ошибки, вызванной неизвестной дереву командой
        Дано во время выполнения команды возникло исключение
        Когда исключение типа SetPosEx, вызванное командой неизвестного типа, обрабатывается
        Тогда срабатывает третий обработчик

    Сценарий: Обработка неизвестной дереву ошибки, вызванной неизвестной дереву командой
        Дано во время выполнения команды возникло исключение
        Когда исключение неизвестного типа, вызванное командой неизвестного типа, обрабатывается
        Тогда срабатывает четвёртый обработчик

       