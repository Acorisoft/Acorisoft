using Acorisoft.Spa;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Studio.ViewModels
{
#pragma warning disable CA1822

    public abstract partial class ViewModelBase : ReactiveObject
    {
        private static IDialogConductor _Conductor;
        private static IScreen _Screen;


        protected static IDialogConductor Conductor
        {
            get
            {
                if(_Conductor is null)
                {
                    _Conductor = Ioc.Get<IDialogConductor>();
                }

                return _Conductor;
            }
        }

        protected static IScreen Screen
        {
            get
            {
                if (_Screen is null)
                {
                    _Screen = Ioc.Get<IScreen>();
                }

                return _Screen;
            }
        }

        public Task<IResultAwaitor> Dialog<TDialog>() where TDialog : IDialogViewModel
        {
            return Conductor.Produce(DialogIntent.Create<TDialog>());
        }


        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4>(TContext context, TStep1 svm1, TStep2 svm2)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
        {

            return Conductor.Produce(DialogIntent.Create(context, svm1, svm2));
        }
        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4>(TContext context, TStep1 svm1, TStep2 svm2, TStep3 svm3)
           where TStep1 : IStepViewModel
           where TStep2 : IStepViewModel
           where TStep3 : IStepViewModel
        {

            return Conductor.Produce(DialogIntent.Create(context, svm1, svm2, svm3));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4>(TContext context, TStep1 svm1, TStep2 svm2, TStep3 svm3, TStep4 svm4)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
        {

            return Conductor.Produce(DialogIntent.Create(context, svm1, svm2, svm3, svm4));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5>(TContext context, TStep1 svm1, TStep2 svm2, TStep3 svm3, TStep4 svm4, TStep5 svm5)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
        {

            return Conductor.Produce(DialogIntent.Create(context, svm1, svm2, svm3, svm4, svm5));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>(TContext context, TStep1 svm1, TStep2 svm2, TStep3 svm3, TStep4 svm4, TStep5 svm5, TStep6 svm6)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
        {

            return Conductor.Produce(DialogIntent.Create(context, svm1, svm2, svm3, svm4, svm5, svm6));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>(TContext context, TStep1 svm1, TStep2 svm2, TStep3 svm3, TStep4 svm4, TStep5 svm5, TStep6 svm6, TStep7 svm7)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
            where TStep7 : IStepViewModel
        {

            return Conductor.Produce(DialogIntent.Create(context, svm1, svm2, svm3, svm4, svm5, svm6, svm7));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>(TContext context, TStep1 svm1, TStep2 svm2, TStep3 svm3, TStep4 svm4, TStep5 svm5, TStep6 svm6, TStep7 svm7, TStep8 svm8)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
            where TStep7 : IStepViewModel
            where TStep8 : IStepViewModel
        {

            return Conductor.Produce(DialogIntent.Create(context,svm1,svm2,svm3,svm4,svm5,svm6,svm7,svm8));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2>(TContext context)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
        {
            return Conductor.Produce(DialogIntent.Create<TContext, TStep1, TStep2>(context));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3>(TContext context)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
        {
            return Conductor.Produce(DialogIntent.Create<TContext, TStep1, TStep2, TStep3>(context));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4>(TContext context)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
        {
            return Conductor.Produce(DialogIntent.Create<TContext, TStep1, TStep2, TStep3, TStep4>(context));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5>(TContext context)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
        {
            return Conductor.Produce(DialogIntent.Create<TContext, TStep1, TStep2, TStep3, TStep4, TStep5>(context));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>(TContext context)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
        {
            return Conductor.Produce(DialogIntent.Create<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6>(context));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>(TContext context)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
            where TStep7 : IStepViewModel
        {
            return Conductor.Produce(DialogIntent.Create<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7>(context));
        }

        public Task<IResultAwaitor> Step<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>(TContext context)
            where TStep1 : IStepViewModel
            where TStep2 : IStepViewModel
            where TStep3 : IStepViewModel
            where TStep4 : IStepViewModel
            where TStep5 : IStepViewModel
            where TStep6 : IStepViewModel
            where TStep7 : IStepViewModel
            where TStep8 : IStepViewModel
        {
            return Conductor.Produce(DialogIntent.Create<TContext, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TStep7, TStep8>(context));
        }
    }
}
