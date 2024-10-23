
#include <stdio.h>
#include "Produto.h"

const char *nome_arquivo_produto = "produto.txt";
FILE *arquivoProduto = NULL;

void abrirArquivoProdutoEscrita() {
    arquivoProduto = fopen(nome_arquivo_produto, "a");
    if (arquivoProduto == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void abrirArquivoProdutoLeitura() {
    arquivoProduto = fopen(nome_arquivo_produto, "r");
    if (arquivoProduto == NULL) {
        printf("O arquivo '%s' não existe. Criando o arquivo...\n", nome_arquivo_produto);

        arquivoProduto = fopen(nome_arquivo_produto, "a");
        if (arquivoProduto == NULL) {
            printf("Erro ao criar o arquivo.\n");
            return;
        }
        fclose(arquivoProduto);
        arquivoProduto = fopen(nome_arquivo_produto, "r");
        if (arquivoProduto == NULL) {
            printf("Erro ao reabrir o arquivo em modo de leitura.\n");
            return;
        }
    }
}

void fecharArquivoProduto() {
    if (arquivoProduto != NULL) {
        fclose(arquivoProduto);
        arquivoProduto = NULL;
    }
}

void gravarDadosEmArquivoProduto(struct Produto produto) {
    fprintf(arquivoProduto, "%d;%s;%f;%f;%d;%d\n", produto.id, produto.nome, produto.valorProduto, produto.valorMinimo, produto.tipoProduto, produto.quantidadeEstoque);
}

void cadastrarProduto(){
    struct Produto produto;
    int c;
    abrirArquivoProdutoEscrita();

    printf("Digite o id do produto\n");
    scanf("%d", &produto.id);

    printf("Digite o nome do produto:\n");
    while ((c = getchar()) != '\n' && c != EOF);
    fgets(produto.nome, sizeof(produto.nome), stdin);
    produto.nome[strcspn(produto.nome, "\n")] = '\0';

    printf("Digite o valor do produto\n");
    scanf("%f", &produto.valorProduto);

    printf("Digite o valor minimo do produto\n");
    scanf("%f", &produto.valorMinimo);

    printf("Digite o tipo do produto\n");
    scanf("%d", &produto.tipoProduto);

    printf("Digite a quantidade no estoque do produto\n");
    scanf("%d", &produto.quantidadeEstoque);

    gravarDadosEmArquivoProduto(produto);
    fecharArquivoProduto();
}

void relatorioProdutos(){

    abrirArquivoProdutoLeitura();
    struct Produto *produtos = NULL;
    int quantidade = 0;
    int capacidade = 2;


    produtos = (struct Produto *)malloc(capacidade * sizeof(struct Produto));
    if (produtos == NULL) {
        printf("Erro ao alocar memória!\n");
        return;
    }


    struct Produto produto;
    while (fscanf(arquivoProduto, "%d;%[^;];%f;%f;%d;%d\n",
                  &produto.id,
                  produto.nome,
                  &produto.valorProduto,
                  &produto.valorMinimo,
                  &produto.tipoProduto,
                  &produto.quantidadeEstoque) == 6) {


        if (quantidade == capacidade) {
            capacidade *= 2;
            produtos = (struct Produto *)realloc(produtos, capacidade * sizeof(struct Produto));
            if (produtos == NULL) {
                printf("Erro ao realocar memória!\n");
                fclose(arquivoProduto);
                return;
            }
        }


        produtos[quantidade] = produto;
        quantidade++;
    }

    fclose(arquivoProduto);


    for (int i = 0; i < quantidade; i++) {
        printf("Id: %d, Nome: %s, Valor do produto: %f, Valor minimo: %f, Tipo do produto: %d, Quantidade em estoque: %d\n",
               produtos[i].id, produtos[i].nome,
               produtos[i].valorProduto, produtos[i].valorMinimo,
               produtos[i].tipoProduto, produtos[i].quantidadeEstoque);
    }


}
