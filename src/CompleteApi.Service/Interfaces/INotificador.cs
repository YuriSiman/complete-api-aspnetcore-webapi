using CompleteApi.Service.Notifications;
using System.Collections.Generic;

namespace CompleteApi.Service.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
