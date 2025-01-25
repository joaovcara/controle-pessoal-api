using Core.V1.Login.Interfaces.Services;
using Core.V1.Login.Services;
using Microsoft.Extensions.DependencyInjection;
using Core.V1.Cadastros.Usuario.Interfaces.Repositories;
using Core.V1.Cadastros.Usuario.Repositories;
using Core.V1.Cadastros.Usuario.Interfaces.Services;
using Core.V1.Cadastros.Usuario.Services;
using Core.V1.Financeiro.DocumentoFinanceiro.Interfaces.Repositories;
using Core.V1.Financeiro.DocumentoFinanceiro.Repositories;
using Core.V1.Financeiro.DocumentoFinanceiro.Interfaces.Services;
using Core.V1.Financeiro.DocumentoFinanceiro.Services;
using Core.V1.Email.SendGrid.Interfaces.Services;
using Core.V1.Email.SendGrid.Services;
using Core.V1.Cadastros.Categoria.Interfaces.Repositories;
using Core.V1.Cadastros.Categoria.Repositories;
using Core.V1.Cadastros.Categoria.Interfaces.Services;
using Core.V1.Cadastros.Categoria.Services;
using Core.V1.Financeiro.Conta.Interfaces.Repositories;
using Core.V1.Financeiro.Conta.Repositories;
using Core.V1.Financeiro.Conta.Interfaces.Services;
using Core.V1.Financeiro.Conta.Services;

namespace Core.IoC
{
    /// <summary>
    /// Classe de injeção de dependência
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<IDocumentoFinanceiroRepository, DocumentoFinanceiroRepository>();
            services.AddScoped<IDocumentoFinanceiroService, DocumentoFinanceiroService>();

            services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddScoped<IBancoService, BancoService>();
            
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            services.AddScoped<ISendGridService, SendGridService>();

            services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

            return services;
        }
    }
}
