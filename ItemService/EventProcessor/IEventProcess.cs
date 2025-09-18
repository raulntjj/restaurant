namespace ItemService.EventProcessor;

public interface IEventProcess
{
  void Process(string message);
}