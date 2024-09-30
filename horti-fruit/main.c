#include <stdlib.h>
#include <locale.h>
#include <string.h>


struct User {
    char nome[10];
    int permissao;
    int login;
    int senha;
};
int main()


{
    setlocale(LC_ALL,"PORTUGUESE");
    struct User funcionarios[10];
    int acessos;
    int login, senha;
    struct User userLogado;



    //parte a ser deixada no arquivo txt, uma conta admin


    //quando tiver o arquivo txt usar um laço para inserir todos que estão cadastrados em uma variável
    //para fazer consulta

    strcpy(funcionarios[0].nome, "admin");
    funcionarios[0].permissao = 1;
    funcionarios[0].login = 123;
    funcionarios[0].senha = 123;




    strcpy(funcionarios[1].nome, "funcionario");
    funcionarios[1].permissao = 1;
    funcionarios[1].login = 345;
    funcionarios[1].senha = 345;


    printf("Bem-vindo ao sistema HORTIFRUIT, entre com suas credenciais\n");
    printf("Digite seu login:");
    scanf("%i",&login);
    printf("Digite sua senha:");
    scanf("%i",&senha);
    for(int x=0;x<=10;x++)
    {
        if(funcionarios[x].login == login && funcionarios[x].senha == senha)
        {
            acessos = funcionarios[x].permissao;
            userLogado = funcionarios[x];

        }
    }



    switch(acessos)
    {
        case 1:
             printf("Bem vindo: %s", userLogado.nome);
             break;
        case 2:
             printf("Área de teste");
             break;
        default:
             printf("Conta inválida");
             break;
    }

    return 0;
}
