using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Empresa.Mvc.ViewModels
{
    public class LoginViewModel //Refere-se ao contato. Aqui somente a tela
    {   //Essas validacoes vao apenas no LoginViewModel. Dominio é o mais simples possível.
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [EmailAddress] //Validacao de front end para o email
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [DataType(DataType.Password)] //Caixa de texto vira um input 
        public string Senha { get; set; }

        //Digitar Crtl+f5

    }
}
