Функция: Создание макрокоманды

    Сценарий: Макрокоманда успешно выпоняется
        Дано зависимость с названием Game.MacroCommand.Move
        Когда макрокоманда составляется
        Тогда макрокоманда успешно выполняется


    Сценарий: Макрокоманда выкидвает исключение при выполнении
        Дано зависимость с названием Game.MacroCommand.Move
        И известно, что одна из команд не выполнится
        Когда макрокоманда составляется
        Тогда выкидывается исключение
