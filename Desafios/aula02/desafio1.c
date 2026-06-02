#include <stdio.h>
#include <stdlib.h>


typedef struct no
{
    int info;
    struct no *prox;
} No;

//Apelidos dados para facilitar a construção do código
typedef No* Elemento;   //Para falar com ou sobre os elementos criados  
typedef No* Cursor;     //Para percorrer a lista com um curr


//Central de controle dos pontos de referência da lista
typedef struct lista
{
    Elemento inicio;
    Elemento fim;
    int tamanho;
}    Controle;

typedef Controle* Lista; // A lista que você passa para as funções

Lista criarLista()
{
    Lista l = (Lista)malloc(sizeof(Controle));
    if (l)
    {
        l->inicio = NULL;
        l->fim = NULL;
        l->tamanho = 0;
    }
    return l;
}

//Cria novo elemento
Elemento novoNo(int info)
{
    Elemento novo = malloc(sizeof(No));
    if(!novo) return NULL;

    novo->info = info;
    novo->prox = NULL;
    return novo;
}


void inserirNoInicio(Lista l, int info)
{
    Elemento novo = novoNo(info);
    if (!novo)
        return;

    novo->prox = l->inicio;
    l->inicio = novo;

    if (l->tamanho == 0)
    {
        l->fim = novo;
    }
    l->tamanho++;
}

void inserirNoFim(Lista l, int info)
{
    Elemento novo = novoNo(info);

    if (l->inicio == NULL)
    {
        l->inicio = novo;
        l->fim = novo;
    }
    else
    {
        Cursor curr = l->inicio;
        while (curr->prox != NULL)
        {
            curr = curr->prox;
        }
        curr->prox = novo;
        l->fim = novo;
    }
    l->tamanho++;
}


void removePrimeiro(Lista l)
{
    if (l->inicio == NULL)
        return; // lista vazia

    Elemento antigo = l->inicio;
    l->inicio = antigo->prox;

    if (l->inicio == NULL)
        l->fim = NULL;

    free(antigo);
    l->tamanho--;
}


void imprimirLista(Lista l)
{
    Cursor curr = l->inicio;
    printf("Lista (%d itens): ", l->tamanho);
    while (curr != NULL)
    {
        printf("%d -> ", curr->info);
        curr = curr->prox;
    }
    printf("NULL\n");
}


void removeUltimo(Lista l)
{
    if (l->fim == NULL)
        return;
    if (l->inicio == l->fim)
    {
        free(l->inicio);
        l->inicio = NULL;
        l->fim = NULL;
        l->tamanho = 0;
        return;
    }

    Elemento penultimo = l->inicio;
    while (penultimo->prox != l->fim)
    {
        penultimo = penultimo->prox;
    }
    free(l->fim);
    penultimo->prox = NULL;
    l->fim = penultimo;
    l->tamanho--;
}


void removePorNumero(Lista l, int valor)
{
    if (l->inicio == NULL)
        return;

    if (l->inicio->info == valor)
    {
        Elemento antigo = l->inicio;
        l->inicio = antigo->prox;

        if (l->inicio == NULL) l->fim = NULL;
        
        free(antigo);
        l->tamanho--;
        return;
    }

    Cursor curr = l->inicio;
    // Percorrer a lista procurando pelo valor
    while (curr->prox != NULL)
    {
        // se o proximo nó tiver o valor
        if (curr->prox->info == valor)
        {
            // alteração dos ponteiros
            Elemento removido = curr->prox;

            // curr vai pro prox prox
            curr->prox = removido->prox;

            // se o proximo do removido for nulo, quer dizer que o removido era o ultimo, então precisa-se
            // trocar o ponteiro do fim para ele
            if (removido == l->fim)
                l->fim = curr;
            // remove
            free(removido);
            l->tamanho--;
            return;
        }
        curr = curr->prox;
    }
}


Lista inverteLista(Lista l)
{
    // cria um novo gerente para a lista invertida
    Lista listaInvertida = criarLista();
    Cursor curr = l->inicio;

    while (curr != NULL)
    {
        inserirNoInicio(listaInvertida, curr->info);
        curr = curr->prox;
    }

    return listaInvertida; // retorna o gerente da lista invertida
}

 
//      ======================================Desafio 2.1==========================================
//           Implementa a função somaLista, que recebe uma lista ligada e retorna a soma da mesma
void SomaLista(Lista l) {
    if (l->inicio == NULL)  {
        printf("Lista vazia. Impossível fazer soma\n");
        return;
    }

     Cursor curr = l->inicio;
    int soma = 0;

        while (curr !=NULL){
        soma += curr->info;
        curr = curr->prox;
    }
    printf("soma: %d\n", soma);
}


int main()
{
    Lista myList = criarLista();
    inserirNoFim(myList, 10);
    inserirNoFim(myList, 20);
    inserirNoFim(myList, 30);
    inserirNoInicio(myList, 85);
    inserirNoInicio(myList, 12);
    inserirNoFim(myList, 4);
    inserirNoFim(myList, 25);
    imprimirLista(myList);
    SomaLista(myList);

}