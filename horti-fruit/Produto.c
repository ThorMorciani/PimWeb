#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include "Headers/Produto.h"


const char *nome_arquivo_produto = "produto.txt";
FILE *arquivoProduto = NULL;
struct Produto produtos[100];
int total_produtos = 0;

void abrirArquivoProduto() {
    arquivoProduto = fopen(nome_arquivo_produto, "a");
    if (arquivoProduto == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void abrirArquivoProdutoLeitura() {
    arquivoProduto = fopen(nome_arquivo_produto, "r");
    if (arquivoProduto == NULL) {
        printf("O arquivo '%s' não existe. Criando o arquivo...\n", nome_arquivo_produto);

        arquivoProduto = fopen(nome_arquivo_produto, "wb");
        if (arquivoProduto == NULL) {
            printf("Erro ao criar o arquivo.\n");
            return;
        }
        fclose(arquivoProduto);
        arquivoProduto = fopen(nome_arquivo_produto, "rb");
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
    abrirArquivoProduto();

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
    printf("1 - Granel\n");
    printf("2 - Kilo\n");
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
void vendaProduto() {

    FILE *tempFile;
    char line[200];
    int encontrado = 0;
    int id, quantidadeVendida;

    printf("Digite o ID do produto: ");
    scanf("%d", &id);
    printf("Digite a quantidade vendida: ");
    scanf("%d", &quantidadeVendida);

    abrirArquivoProdutoLeitura();
    tempFile = fopen("temp.txt", "w");
    if (tempFile == NULL) {
        printf("Erro ao abrir o arquivo temporário para escrita.\n");
        fclose(arquivoProduto);
        return;
    }

    while (fgets(line, sizeof(line), arquivoProduto) != NULL) {
        struct Produto produto;
        if (sscanf(line, "%d;%[^;];%f;%f;%d;%d",
                   &produto.id, produto.nome, &produto.valorProduto, &produto.valorMinimo, &produto.tipoProduto, &produto.quantidadeEstoque) == 6) {

            if (produto.id == id) {
                    if((produto.quantidadeEstoque-quantidadeVendida)<0)
                    {
                        printf("Quantidade insuficiente para esta venda");
                        encontrado = 1;
                    } else
                    {
                        produto.quantidadeEstoque -= quantidadeVendida;
                        encontrado = 2;

                    }
          }

            fprintf(tempFile, "%d;%s;%f;%f;%d;%d\n",
                    produto.id, produto.nome, produto.valorProduto, produto.valorMinimo, produto.tipoProduto, produto.quantidadeEstoque);
        } else {
            fputs(line, tempFile);
        }
    }


    fecharArquivoProduto();
    fclose(tempFile);

    if (encontrado == 2) {
        remove(nome_arquivo_produto);
        rename("temp.txt", nome_arquivo_produto);
        printf("Quantidade do produto com ID %d atualizada com sucesso.\n", id);
    }
     else if((encontrado!=2) && (encontrado!=1)){
        remove("temp.txt");
        printf("Produto com ID %d não encontrado.\n", id);
    }

}
void adicionarEstoqueProduto() {

    FILE *tempFile;
    char line[200];
    int encontrado = 0;

    int id, quantidadeAcrescentada;

    printf("Digite o ID do produto: ");
    scanf("%d", &id);
    printf("Digite a quantidade a ser acrescentada: ");
    scanf("%d", &quantidadeAcrescentada);

    abrirArquivoProdutoLeitura();
    tempFile = fopen("temp.txt", "w");
    if (tempFile == NULL) {
        printf("Erro ao abrir o arquivo temporário para escrita.\n");
        fclose(arquivoProduto);
        return;
    }

    while (fgets(line, sizeof(line), arquivoProduto) != NULL) {
        struct Produto produto;

        if (sscanf(line, "%d;%[^;];%f;%f;%d;%d",
                   &produto.id, produto.nome, &produto.valorProduto, &produto.valorMinimo, &produto.tipoProduto, &produto.quantidadeEstoque) == 6) {

            if (produto.id == id) {
                produto.quantidadeEstoque += quantidadeAcrescentada;
                encontrado = 1;
            }
            fprintf(tempFile, "%d;%s;%f;%f;%d;%d\n",
                    produto.id, produto.nome, produto.valorProduto, produto.valorMinimo, produto.tipoProduto, produto.quantidadeEstoque);
        } else {
            fputs(line, tempFile);
        }
    }

    fecharArquivoProduto();
    fclose(tempFile);

    if (encontrado) {
        remove(nome_arquivo_produto);
        rename("temp.txt", nome_arquivo_produto);
        printf("Quantidade do produto com ID %d atualizada com sucesso.\n", id);
    } else {
        remove("temp.txt");
        printf("Produto com ID %d não encontrado.\n", id);
    }

}

void removerEstoqueProduto(int id, int qtdVentida) {

    FILE *tempFile;
    char line[200];
    int encontrado = 0;

    abrirArquivoProdutoLeitura();
    struct Produto produto = buscarProdutoPorId(id);

    fecharArquivoProduto();
    abrirArquivoProduto();

    while (fgets(line, sizeof(line), arquivoProduto) != NULL) {
        struct Produto produto;

        if (sscanf(line, "%d;%[^;];%f;%f;%d;%d",
                   &produto.id, produto.nome, &produto.valorProduto, &produto.valorMinimo, &produto.tipoProduto, &produto.quantidadeEstoque) == 6) {

            if (produto.id == id) {
                produto.quantidadeEstoque = qtdVentida;
                encontrado = 1;
            }
            gravarDadosEmArquivoProduto(produto);
        } else {
            fputs(line, arquivoProduto);
        }
    }

    fecharArquivoProduto();

    if (encontrado) {
        remove(nome_arquivo_produto);
        rename("temp.txt", nome_arquivo_produto);
        printf("Quantidade do produto com ID %d atualizada com sucesso.\n", id);
    } else {
        printf("Produto com ID %d não encontrado.\n", id);
    }

}

void alterarValorProduto() {

    FILE *tempFile;
    char line[200];
    int encontrado = 0;

    int id;
    float novoValor;

    printf("Digite o ID do produto: ");
    scanf("%d", &id);
    printf("Digite o novo valor: ");
    scanf("%f", &novoValor);

    abrirArquivoProdutoLeitura();
    tempFile = fopen("temp.txt", "w");
    if (tempFile == NULL) {
        printf("Erro ao abrir o arquivo temporário para escrita.\n");
        fclose(arquivoProduto);
        return;
    }

    while (fgets(line, sizeof(line), arquivoProduto) != NULL) {
        struct Produto produto;

        if (sscanf(line, "%d;%[^;];%f;%f;%d;%d",
                   &produto.id, produto.nome, &produto.valorProduto, &produto.valorMinimo, &produto.tipoProduto, &produto.quantidadeEstoque) == 6) {

            if ((produto.id == id) && (novoValor >= produto.valorMinimo)) {
                produto.valorProduto = novoValor;
                encontrado = 1;
            } else if ((produto.id == id) && (novoValor < produto.valorMinimo))
            {
                encontrado = 2;
            } else
            {
                encontrado = 0;
            }
            fprintf(tempFile, "%d;%s;%f;%f;%d;%d\n",
                    produto.id, produto.nome, produto.valorProduto, produto.valorMinimo, produto.tipoProduto, produto.quantidadeEstoque);
        } else {
            fputs(line, tempFile);
        }
    }

    fecharArquivoProduto();
    fclose(tempFile);

    if (encontrado == 1)
    {
        remove(nome_arquivo_produto);
        rename("temp.txt", nome_arquivo_produto);
        printf("valor do produto com ID %d atualizada com sucesso.\n", id);
    } else if(encontrado == 2)
    {
        remove("temp.txt");
        printf("Valor abaixo do valor minimo predefinido");
    } else
    {
        printf("Produto com ID %d não encontrado", id);
    }

}

void excluirProduto() {

    FILE *tempFile;
    char line[200];
    int encontrado = 0;

    int id;

    printf("Digite o ID do produto: ");
    scanf("%d", &id);

    abrirArquivoProdutoLeitura();
    tempFile = fopen("temp.txt", "w");
    if (tempFile == NULL) {
        printf("Erro ao abrir o arquivo temporário para escrita.\n");
        fclose(arquivoProduto);
        return;
    }

    while (fgets(line, sizeof(line), arquivoProduto) != NULL) {
        struct Produto produto;

        if (sscanf(line, "%d;%[^;];%f;%f;%d;%d",
                   &produto.id, produto.nome, &produto.valorProduto, &produto.valorMinimo, &produto.tipoProduto, &produto.quantidadeEstoque) == 6) {

            if (produto.id == id)
            {
                encontrado = 1;
                continue;
            }
            fprintf(tempFile, "%d;%s;%f;%f;%d;%d\n",
                    produto.id, produto.nome, produto.valorProduto, produto.valorMinimo, produto.tipoProduto, produto.quantidadeEstoque);
        } else {
            fputs(line, tempFile);
        }


        fecharArquivoProduto();
        fclose(tempFile);

        if (encontrado) {
            remove(nome_arquivo_produto);
            rename("temp.txt", nome_arquivo_produto);
            printf("Produto com ID %d deletado com sucesso.\n", id);
        } else {
            printf("Produto com ID %d não encontrado", id);
        }
    }
}

void carregarProdutos() {
    abrirArquivoProdutoLeitura();

    while (fscanf(arquivoProduto, "%d;%49[^;];%f;%f;%d;%d\n",
                  &produtos[total_produtos].id,
                  produtos[total_produtos].nome,
                  &produtos[total_produtos].valorProduto,
                  &produtos[total_produtos].valorMinimo,
                  &produtos[total_produtos].tipoProduto,
                  &produtos[total_produtos].quantidadeEstoque) != EOF) {
        total_produtos++;
    }

    fecharArquivoProduto();
}

struct Produto buscarProdutoPorId(int id) {
    carregarProdutos();
    for (int i = 0; i < total_produtos; i++) {
        if (produtos[i].id == id) {
            return produtos[i];
        }
    }
}
