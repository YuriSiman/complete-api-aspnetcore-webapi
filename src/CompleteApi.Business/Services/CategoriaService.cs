﻿using CompleteApi.Business.Interfaces;
using CompleteApi.Business.Models;
using CompleteApi.Business.Models.Validations.Categorias;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Business.Services
{
    public class CategoriaService : MainService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository, INotificador notificador) : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Adicionar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;

            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Remover(Guid id)
        {
            await _categoriaRepository.Remover(id);
        }

        public void Dispose()
        {
            _categoriaRepository?.Dispose();
        }
    }
}
