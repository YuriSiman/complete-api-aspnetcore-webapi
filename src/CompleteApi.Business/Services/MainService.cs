using CompleteApi.Business.Interfaces;
using CompleteApi.Business.Models;
using CompleteApi.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace CompleteApi.Business.Services
{
    public abstract class MainService
    {
        private readonly INotificador _notificador;

        public MainService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }
        
        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notificar(validator);
            return false;
        }
    }
}
