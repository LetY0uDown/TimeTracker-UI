using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core;

internal static class WorkTimeCounter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actions">Действия, произведённые над задачаей</param>
    /// <returns>Время работы над задачей</returns>
    public static TimeSpan CalculateWorkTime (List<TaskAction> actions)
    {
        // Если нет действий, то время = ноль
        if (actions is null || actions.Count == 0) {
            return new (0);
        }

        long totalTime = 0;
        var lastAction = actions[0];

        foreach (var action in actions) {
            switch (action.Type!.Id) {
                case TaskActionType.Kind.Start: continue;

                // Если нынешнее действие - постановка на паузу,
                // значит в период после прешлого действия над задачей была проделана работа.
                // Соответственно считаем время, которое прошло между нынешним и прошлым действием, и прибавляем к общему
                case TaskActionType.Kind.Pause:
                    totalTime += action.CreatedAt - lastAction.CreatedAt;
                    lastAction = action;
                break;

                case TaskActionType.Kind.Resume: break;

                // Если действие - завершение работы,
                // значит так же либо прибавляем время после прошлого действия,
                // либо не прибавляем, если прошлое действия - пауза, т.к. над задачей не работал
                case TaskActionType.Kind.Finish:
                    if (lastAction.Type!.Id == TaskActionType.Kind.Pause) {
                        break;
                    }
                    
                    totalTime += action.CreatedAt - lastAction.CreatedAt;
                break;
            }

            lastAction = action;
        }

        TimeSpan time = new (totalTime);

        // Создаём новые экземплер чтобы "округлить" время и избавиться от лишних микросекунд
        return new(time.Hours, time.Minutes, time.Seconds);
    }
}