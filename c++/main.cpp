#include <iostream>
#include <vector>
#include <fstream>

class Student
{
    std::string m_first = "First";
    std::string m_last = "Last";
    int m_id = 0;
    float m_avg = 0;

public:

Student(std::string first, std::string last, int id, float avg)
    : m_first (first)
    , m_last (last)
    , m_id (id)
    , m_avg (avg)
{
}

Student() {}

int getAvg() const
{
    return m_avg;
}

int getID() const
{
    return m_id;
}

std::string getFirst() const
{
    return m_first;
}

std::string getLast() const
{
    return m_last;
}

void print() const
{
    std::cout << m_first << " " << m_last << " " << m_id << " " << m_avg << "\n";
}

};


class Course
{
    std::string m_name = "Course";
    std::vector<Student> m_students;

public:

    Course() {}

    Course(const std::string& name)
        : m_name (name)
    {
    } 

    void addStudent(const Student& s)
    {
        m_students.push_back(s);
    }

    const std::vector<Student>& getStudents() const
    {
        return m_students;
    }

    void loadFromFile(const std::string& filename)
    {
        std::ifstream fin(filename);
        std::string first, last;
        int id;
        float avg;

        while (fin >> first)
        {
            fin >> last >> id >> avg;

            addStudent(Student(first, last, id, avg));
        }
        

    }

    void print() const 
    {
        for (const auto& s : m_students)
        {
            s.print();
        }
    }

};

int main(){
    auto result = (10 <= 20) > 0;
    std::cout << result << std::endl;
    std::cout << "Hello, world!" << std::endl;
    std::cout << "Yoo!!"  << std::endl;   
    std::cout << "Yofdfvo!!"  << std::endl;

    std::vector<int> vec;
    vec.push_back(10);
    vec.push_back(42);

    std::cout << vec[0] << "\n";
    std::cout << vec[1] << "\n";

    for (size_t i = 0; i < vec.size(); i++)
    {
        std::cout << vec[i] << "\n";
    }

    for (int a : vec)
    {
        std::cout << a << "\n";
    }

    Student s1;
    Student s2("Dave", "Churchill", 1, 3.14);
    Student s3("Jane", "Doe", 202200001, 99.9);


    std::cout << s3.getLast() << "\n";
    std::cout << "\n";

    Course comp4300("COMP 4300");
    comp4300.addStudent(s1);
    comp4300.addStudent(s2);
    comp4300.addStudent(s3);
    comp4300.addStudent(Student("Janes", "Go", 2022, 90.9));

    comp4300.print();

    Course comp4301("Comp 4301");

    comp4301.loadFromFile("students.txt");
    comp4301.print();

    return 0;
}