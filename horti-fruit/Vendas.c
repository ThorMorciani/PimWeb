#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>
#include "Headers/Vendas.h"

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
    FILE *file = fopen(filename, "a");
    if (file == NULL) {
        perror("Erro ao abrir o arquivo");
        return;
    }

    char date[11];
    Pegar_Data_Atual(date);

    fprintf(file, "[%s] %s\n", date, line);
    fclose(file);
}


void ler_linhas_pela_data(const char *filename, const char *date) {
    FILE *file = fopen(filename, "r");
    if (file == NULL) {
        perror("Erro ao abrir o arquivo");
        return;
    }

    char buffer[256];
    int found = 0;
    while (fgets(buffer, sizeof(buffer), file) != NULL) {
        if (strncmp(buffer + 1, date, 10) == 0) {
            printf("%s", buffer);
            found = 1;
        }
    }

    if (!found) {
       printf("Nenhuma venda encontrada para a data %s\n", date);
    }

    fclose(file);
}


double soma_valores_na_data(const char *filename, const char *date) {
    FILE *file = fopen(filename, "r");
    if (file == NULL) {
        perror("Erro ao abrir o arquivo");
        return 0;
    }

    char linha[256];
    double soma = 0;
    while (fgets(linha, sizeof(linha), file)) {
        if (strncmp(linha + 1, date, 10) == 0) {
            char *token = strtok(linha + 12, ",");
            while (token != NULL) {
                soma += atof(token);
                token = strtok(NULL, ",");
            }
        }
    }

    fclose(file);
    return soma;
}


void gerar_relatorio_na_data(const char *filename, const char *date) {
    double total = soma_valores_na_data(filename, date);

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

int main() {
    const char *filename = "vendas.txt";
    char date[11];

    printf("Digite a data no formato DD/MM/YYYY: ");
    scanf("%10s", date);

    if (!validar_data(date)) {
        printf("Erro: formato de data inválido.\n");
        return 1;
    }

    printf("\n\nVendas da data %s:\n\n", date);
    ler_linhas_pela_data(filename, date);

    gerar_relatorio_na_data(filename, date);

    return 0;
}