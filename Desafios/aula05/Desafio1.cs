// Nó da árvore (conforme fornecido)
public class Node
{
    public int Key { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }
    
    public Node(int key)
    {
        this.Key = key;
    }
}

// BST é Binary Search Tree
public class BST
{
 public Node Root { get; set; }

 public class BST()
 {
     this.Root() = Null;
 }

 // Método Público: Inserir
   public void Insert(int valor){
        // A raiz da árvore pode mudar se ela estiver vazia, 
        // por isso reatribuímos a Root com o resultado da recursão.
        Root = InsertRecursive(Root, valor);
    }

   private Node InsertRecursive(Node atual, int valor)
   {
       //Caso base: No caso de não haver nó algum, cria um nó 
       if (atual == NULL)
       {
           return new Node(valor);
       }

       if (valor < atual.Key)
       {
           atual.Left = InsertRecursive(atual.Left, valor);
       }

       else if (valor > atual.Key)
       {
           atual.Right = InsertRecursive(atual.Right, valor);
       }

       return atual;
   }

    //Buscar
    public Node Search(int value)
    {
        return SearchRecursive(Root, value);
    }

    //Busca Recursiva
    private Node SearchRecursive(Node atual, int valor)
    {
        if (atual == null)
        {
            Console.WriteLine("não encontrado");
            return atual;
        }
        elif(atual.Key == valor)
        {
            return atual;
        }
        if (valor < atual.Key)
        {
            return SearchRecursive(atual.Left, valor);
        }

        return SearchRecursive(atual.Right, valor);
    }
}

