using Stack_C_;

CStack pila = new CStack();

pila.Push(5);
pila.Push(3);
pila.Push(10);

pila.Transversa();

Console.WriteLine(pila.Pop());

pila.Transversa();

Console.WriteLine(pila.Peek());
Console.WriteLine(pila.Peek());
pila.Transversa();
