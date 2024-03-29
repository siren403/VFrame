﻿using System;
using VFrame.UI.Animation;
using VFrame.UI.Context;
using VFrame.UI.Group;
using VFrame.UI.Transition;
using VFrame.UI.View;

namespace VFrame.UI.Matcher
{
    public class GroupMatcher<TView, TGroup> : IViewMatcher<IGroup>
        where TView : class, IView
        where TGroup : class, IGroup
    {
        public Type ViewType => typeof(TView);
        public IGroup Resolve(ISystemContext context) => context.ResolveGroup<TGroup>();
    }

    public class AnimationMatcher<TView, TAnimation> : IViewMatcher<IAnimation>
        where TView : class, IView
        where TAnimation : class, IAnimation
    {
        public Type ViewType => typeof(TView);

        public IAnimation Resolve(ISystemContext context) => context.Resolve<TAnimation>();
    }

    public class TransitionMatcher<TView, TTransition> : IViewMatcher<ITransition>
        where TView : class, IView
        where TTransition : class, ITransition<TView>
    {
        public Type ViewType => typeof(TView);
        public ITransition Resolve(ISystemContext context) => context.Resolve<TTransition>();
    }
}