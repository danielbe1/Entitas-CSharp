using Entitas;

public class ContextCreateEntity : IPerformanceTest {

    const int n = 100000;
    Helper.Context<Entity> _context;

    public void Before() {
        _context = Helper.CreateContext();
    }

    public void Run() {
        for (int i = 0; i < n; i++) {
            _context.CreateEntity();
        }
    }
}
