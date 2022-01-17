using Cysharp.Threading.Tasks;
using VFrame.UI.Context;

namespace VFrame.UI.Command
{
    public class PopCommand : ICommand
    {
        public async UniTask Execute(ISystemContext context)
        {
            if (!context.View.SafetyAny()) return;

            var outView = context.View.Pop();
            var outAnimation = context.ResolveAnimation(outView);

            if (context.View.Any())
            {
                var restoreView = context.View.Peek();
                if (restoreView.IsActive == false)
                {
                    await restoreView.Ready();
                    restoreView.IsActive = true;
                    var restoreAnimation = context.ResolveAnimation(restoreView);

                    await UniTask.WhenAll(
                        outAnimation.Out(outView),
                        restoreAnimation.In(restoreView)
                    );
                    
                    outView.IsActive = false;
                    outView.OnExit();
                    restoreView.OnEnter();
                }
                else
                {
                    await outAnimation.Out(outView);
                    outView.IsActive = false;
                    outView.OnExit();
                }
            }
            else
            {
                await outAnimation.Out(outView);
                outView.IsActive = false;
                outView.OnExit();
            }
        }
    }

    public class PopParallelCommand : ICommand
    {
        public async UniTask Execute(ISystemContext context)
        {
            if (!context.View.SafetyAny()) return;

            var outView = context.View.Pop();
            var outAnimation = context.ResolveAnimation(outView);

            if (context.View.Any())
            {
                UniTask outTask;
                var restoreView = context.View.Peek();
                if (restoreView.IsActive == false)
                {
                    await restoreView.Ready();
                    restoreView.IsActive = true;
                    var restoreAnimation = context.ResolveAnimation(restoreView);

                    outTask = UniTask.WhenAll(
                        outAnimation.Out(outView),
                        restoreAnimation.In(restoreView)
                    );
                }
                else
                {
                    outTask = outAnimation.Out(outView);
                }

                outTask.ContinueWith(() =>
                {
                    outView.IsActive = false;
                    outView.OnExit();
                    restoreView.OnEnter();
                });
            }
            else
            {
                outAnimation.Out(outView).ContinueWith(() =>
                {
                    outView.IsActive = false;
                    outView.OnExit();
                });
            }
        }
    }
}