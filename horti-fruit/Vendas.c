#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>
#include "Headers/Vendas.h"
#include "Headers/Produto.h"

const char *nome_arquivo_vendas = "vendas.txt";
FILE *arquivoVendas = NULL;

void abrirArquivoVendas() {
    arquivoVendas = fopen(nome_arquivo_vendas, "a");
    if (arquivoVendas == NULL) {
        printf("Erro ao abrir o arquivo!\n");
    }
}

void abrirArquivoVendasLeitura() {
    arquivoVendas = fopen(nome_arquivo_vendas, "r");
    if (arquivoVendas == NULL) {
        printf("O arquivo '%s' não existe. Criando o arquivo...\n", nome_arquivo_vendas);

        arquivoVendas = fopen(nome_arquivo_vendas, "wb");
        if (arquivoVendas == NULL) {
            printf("Erro ao criar o arquivo.\n");
            return;    
        }
        fclose(arquivoVendas);
        arquivoVendas = fopen(nome_arquivo_vendas, "rb");
        if (arquivoVendas == NULL) {
            printf("Erro ao reabrir o arquivo em modo de leitura.\n");
            return;
        }
    }
}

void fecharArquivoVendas() {
    if (arquivoVendas != NULL) {
        fclose(arquivoVendas);
        arquivoVendas = NULL;
    }
}

void Pegar_Data_Atual(char *buffer) {
    time_t t = time(NULL);
    struct tm *tm_info = localtime(&t);
    strftime(buffer, 11, "%d/%m/%Y", tm_info);
}


void add_linha_layout(const char *filename, const char *line) {
    abrirArquivoVendas();
    char date[11];
    Pegar_Data_Atual(date);

    fprintf(arquivoVendas, "[%s] %s\n", date, line);
    fecharArquivoVendas();
}


void ler_linhas_pela_data(const char *date) {
    abrirArquivoVendasLeitura();

    char buffer[256];
    int found = 0;
    while (fgets(buffer, sizeof(buffer), arquivoVendas) != NULL) {
        if (strncmp(buffer + 1, date, 10) == 0) {
            printf("%s", buffer);
            found = 1;
        }
    }

    if (!found) {
       printf("Nenhuma venda encontrada para a data %s\n", date);
    }

    fecharArquivoVendas();
}


double soma_valores_na_data(const char *date) {
    abrirArquivoVendasLeitura();
    char linha[256];
    double soma = 0;
    while (fgets(linha, sizeof(linha), arquivoVendas)) {
        if (strncmp(linha + 1, date, 10) == 0) {
            char *token = strtok(linha + 12, ",");
            while (token != NULL) {
                soma += atof(token);
                token = strtok(NULL, ",");
            }
        }
    }

    fecharArquivoVendas();
    return soma;
}


void gerar_relatorio_na_data(const char *date) {
    double total = soma_valores_na_data(date);

  if (total == 0) {
        printf(" Relatório não pode ser gerado\n");
    } else {
        printf("\nRelatório de Vendas para a Data %s\n", date);
        printf("\n=================================\n");
        printf("\nTotal de vendas: %.2f\n", total);
    }
}

int validar_data(const char *date) {
    if (strlen(date) != 10) return 0;
    if (date[2] != '/' || date[5] != '/') return 0;
    for (int i = 0; i < 10; i++) {
        if (i == 2 || i == 5) continue;
        if (date[i] < '0' || date[i] > '9') return 0;
    }
    return 1;
}

void gravarDadosEmArquivoVendas(struct Produto produto, int quantidade, struct Venda venda) {
    abrirArquivoVendas();
    if (arquivoVendas != NULL) {
        float valorVenda = produto.valorProduto * quantidade;
        venda.valorTotal = valorVenda;
        fprintf(arquivoVendas, "%d;%s;%s;%.2f;%d;%s;%.2f;%d\n",
                venda.id, venda.docVendedor, venda.docCliente, valorVenda,
                produto.id, produto.nome, produto.valorProduto, quantidade);
        fecharArquivoVendas();
    }
}

void cadastrarVenda() {
    int idProduto, quantidade;
    struct Venda venda;
    
    printf("Digite o ID do produto: ");
    scanf("%d", &idProduto);
    
    struct Produto produto = buscarProdutoPorId(idProduto);

    printf("Digite a quantidade desejada: ");
    scanf("%d", &quantidade);

    if (quantidade > produto.quantidadeEstoque) {
        printf("Estoque insuficiente. Quantidade disponível: %d\n", produto.quantidadeEstoque);
        return;
    }

    printf("Digite o documento do vendedor: ");
    scanf("%s", venda.docVendedor);
    printf("Digite o documento do cliente: ");
    scanf("%s", venda.docCliente);

    venda.id = idProduto;
    gravarDadosEmArquivoVendas(produto, quantidade, venda);
    printf("Venda registrada com sucesso!\n");

    produto.quantidadeEstoque -= quantidade;

    removerEstoqueProduto(produto.id, produto.quantidadeEstoque);
}

void exibirRelatorioVendas() {
    FILE *arquivoVendas = fopen(nome_arquivo_vendas, "r");
    if (arquivoVendas == NULL) {
        printf("Nenhuma venda registrada.\n");
        return;
    }

    printf("ID Venda | Vendedor     | Cliente      | Valor Total | ID Produto | Nome Produto       | Valor Unitário | Quantidade\n");
    printf("---------------------------------------------------------------------------------------------------------------\n");

    struct Venda venda;
    struct Produto produto;
    int quantidade;
    while (fscanf(arquivoVendas, "%d;%19[^;];%19[^;];%f;%d;%49[^;];%f;%d\n",
                  &venda.id, venda.docVendedor, venda.docCliente, &venda.valorTotal,
                  &produto.id, produto.nome, &produto.valorProduto, &quantidade) != EOF) {
        printf("%-8d | %-12s | %-12s | %-11.2f | %-10d | %-18s | %-13.2f | %d\n",
               venda.id, venda.docVendedor, venda.docCliente, venda.valorTotal,
               produto.id, produto.nome, produto.valorProduto, quantidade);
    }

    fclose(arquivoVendas);
}