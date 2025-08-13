#include <iostream>

void pp(int & a)
{
    std::cout << &a << " " << a << " " << sizeof(a) << "\n";
}

int main()
{
    int a = 10;
    int b = 25;

    int arr[10];

    pp(a);
    pp(b);

    for (size_t i = 0; i < 10; i++)
    {
        pp(arr[i]);
    }

    return 0;
}