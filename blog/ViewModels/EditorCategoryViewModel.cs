using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels;

// OBS: ViewModels são ClassesDeModelo se baseando na entrada do front-end e facilitando para ele, assim podemos fazer validações separadas das modelos originais onde fica a regra de negócio, O certo é criar uma CLASSE/MODEL para cada "Ação" que iremos fazer, geralmente o nome da classe é NomeDaAçãoNomeModelViewModel, porém quando temos uma classe que tem as mesmas propriedades de outras "ações" colocamos o nome dela de EditorNomeModelViewModel e usamos ela em mais de uma ação/action/endpoint.
public class EditorCategoryViewModel
{
    // Também podemos usar os attributes/anotations para fazer validações, o próprio ASPNET utiliza o MODELSTATE para verificar se o objeto é válido com base nas anonations/attributes/validações que nós fazemos no model.
    [StringLength(60,MinimumLength = 3,ErrorMessage ="Este Campo deve conter entre 3 e 60 caracteres")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }   

    [Required(ErrorMessage = "O slug é obrigatório")]
    public string Slug { get; set; }
}
