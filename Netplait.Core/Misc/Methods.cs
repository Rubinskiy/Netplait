using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ScintillaNET;

namespace Netplait.Core.Misc
{
    public class Methods
    {
        public static string[] WordList = { "abs", "all", "any", "ascii", "bin", "bool", "bytearray", "bytes", "callable", "chr", "classmethod", "compile",
        "complex", "delattr", "dict", "dir", "divmod", "enumerate", "eval", "exec", "filter", "float", "format", "frozenset", "getattr", "globals",
        "hasattr", "hash", "help", "hex", "id", "input", "int", "isinstance", "issubclass", "iter", "len", "list", "locals", "map", "max",
        "memoryview", "min", "next", "object", "oct", "open", "ord", "pow", "print", "property", "range", "repr", "reversed", "round", "set",
        "setattr", "slice", "sorted", "staticmethod", "str", "sum", "super", "tuple", "type", "vars", "zip" };

        public static string Hover(string str, ScintillaNET.Scintilla TextArea)
        {
            switch (str)
            {
                case "abs":
                    return "abs() Function - Returns the absolute value of a number." + "\n" +
                        @"abs(n)";
                case "all":
                    return "all() Function - Returns True if all items in an iterable object are true." + "\n" +
                        @"all(iterable)";
                case "any":
                    return "any() Function - Returns True if any item in an iterable object is true." + "\n" +
                        @"any(iterable)";
                case "ascii":
                    return "ascii() Function - Returns a readable version of an object. Replaces none-ascii characters with escape character." + "\n" +
                        @"ascii(object)";
                case "bin":
                    return "bin() Function - Returns the binary version of a number." + "\n" +
                        @"bin(n)";
                case "bool":
                    return "bool() Function - Returns the boolean value of the specified object." + "\n" +
                        @"bool(object)";
                case "bytearray":
                    return "bytearray() Function - Returns an array of bytes." + "\n" +
                        @"bytearray(x, encoding, error)";
                case "bytes":
                    return "bytes() Function - Returns a bytes object." + "\n" +
                        @"bytes(x, encoding, error)";
                case "callable":
                    return "callable() Function - Returns True if the specified object is callable, otherwise False." + "\n" +
                        @"callable(object)";
                case "chr":
                    return "chr() Function - Returns a character from the specified Unicode code." + "\n" +
                        @"chr(number)";
                case "classmethod":
                    return "classmethod() Function - Converts a method into a class method." + "\n" +
                        @"classmethod(function) -> classmethod";
                case "compile":
                    return "compile() Function - Returns the specified source as an object, ready to be executed." + "\n" +
                        @"compile(source, filename, mode, flag, dont_inherit, optimize)";
                case "complex":
                    return "complex() Function - Returns a complex number." + "\n" +
                        @"complex(real, imaginary)";
                case "delattr":
                    return "delattr() Function - Deletes the specified attribute (property or method) from the specified object." + "\n" +
                        @"delattr(object, attribute)";
                case "dict":
                    return "dict() Function - Returns a dictionary (Array)." + "\n" +
                        @"dict(keyword arguments)";
                case "dir":
                    return "dir() Function - Returns a list of the specified object's properties and methods." + "\n" +
                        @"dir(object)";
                case "divmod":
                    return "divmod() Function - Returns the quotient and the remainder when argument1 is divided by argument2." + "\n" +
                        @"divmod(divident, divisor)";
                case "enumerate":
                    return "enumerate() Function - Takes a collection (e.g. a tuple) and returns it as an enumerate object." + "\n" +
                        @"enumerate(iterable, start)";
                case "eval":
                    return "eval() Function - Evaluates and executes an expression." + "\n" +
                        @"eval(expression, globals, locals)";
                case "exec":
                    return "exec() Function - Executes the specified code (or object)." + "\n" +
                        @"exec(object, globals, locals)";
                case "filter":
                    return "filter() Function - Use a filter function to exclude items in an iterable object." + "\n" +
                        @"filter(function, iterable)";
                case "float":
                    return "float() Function - Returns a floating point number." + "\n" +
                        @"float(value)";
                case "format":
                    return "format() Function - Formats a specified value." + "\n" +
                        @"format(value, format)";
                case "frozenset":
                    return "frozenset() Function - Returns a frozenset object." + "\n" +
                        @"frozenset(iterable)";
                case "getattr":
                    return "getattr() Function - Returns the value of the specified attribute (property or method)." + "\n" +
                        @"getattr(object, attribute, default)";
                case "globals":
                    return "globals() Function - Returns the current global symbol table as a dictionary." + "\n" +
                        @"globals()";
                case "hasattr":
                    return "hasattr() Function - Returns True if the specified object has the specified attribute (property/method)." + "\n" +
                        @"hasattr(object, attribute)";
                case "hash":
                    return "hash() Function - Returns the hash value of a specified object." + "\n" +
                        @"hash(object)";
                case "help":
                    return "help() Function - Executes the built-in help system." + "\n" +
                        @"help()";
                case "hex":
                    return "hex() Function - Converts a number into a hexadecimal value." + "\n" +
                        @"hex(number)";
                case "id":
                    return "id() Function - Returns the id of an object." + "\n" +
                        @"id(object)";
                case "input":
                    return "input() Function - Allowing user input." + "\n" +
                        @"input(prompt)";
                case "int":
                    return "int() Function - Returns an integer number." + "\n" +
                        @"int(value, base)";
                case "isinstance":
                    return "isinstance() Function - Returns True if a specified object is an instance of a specified object." + "\n" +
                        @"isinstance(object, type)";
                case "issubclass":
                    return "issubclass() Function - Returns True if a specified class is a subclass of a specified object." + "\n" +
                        @"issubclass(object, subclass)";
                case "iter":
                    return "iter() Function - Returns an iterator object." + "\n" +
                        @"iter(object, sentinel)";
                case "len":
                    return "len() Function - Returns the length of an object." + "\n" +
                        @"len(object)";
                case "list":
                    return "list() Function - Returns a list." + "\n" +
                        @"list(iterable)";
                case "locals":
                    return "locals() Function - Returns an updated dictionary of the current local symbol table." + "\n" +
                        @"locals()";
                case "map":
                    return "map() Function - Returns the specified iterator with the specified function applied to each item." + "\n" +
                        @"map(function, iterables)";
                case "max":
                    return "max() Function - Returns the largest item in an iterable." + "\n" +
                        @"max(iterable)";
                case "memoryview":
                    return "memoryview() Function - Returns a memory view object." + "\n" +
                        @"memoryview(obj)";
                case "min":
                    return "min() Function - Returns the smallest item in an iterable." + "\n" +
                        @"min(iterable)";
                case "next":
                    return "next() Function - Returns the next item in an iterable." + "\n" +
                        @"next(iterable, default)";
                case "object":
                    return "object() Function - Returns a new object." + "\n" +
                        @"object()";
                case "oct":
                    return "oct() Function - Converts a number into an octal." + "\n" +
                        @"oct(int)";
                case "open":
                    return "open() Function - Opens a file and returns a file object." + "\n" +
                        @"open(file, mode)";
                case "ord":
                    return "ord() Function - Convert an integer representing the Unicode of the specified character." + "\n" +
                        @"ord(character)";
                case "pow":
                    return "pow() Function - Returns the value of x to the power of y." + "\n" +
                        @"pow(x, y, z)";
                case "print":
                    return "print() Function - Prints to the standard output device." + "\n" +
                        @"print(object(s), sep=separator, end=end, file=file, flush=flush)";
                case "property":
                    return "property() Function - Gets, sets, deletes a property." + "\n" +
                        @"property()";
                case "range":
                    return "range() Function - Returns a sequence of numbers, starting from 0 and increments by 1 (by default)." + "\n" +
                        @"range(start, stop, step)";
                case "repr":
                    return "repr() Function - Returns a readable version of an object." + "\n" +
                        @"repr(object)";
                case "reversed":
                    return "reversed() Function - Returns a reversed iterator." + "\n" +
                        @"reversed(sequence)";
                case "round":
                    return "round() Function - Rounds a number." + "\n" +
                        @"round(number, digits)";
                case "set":
                    return "set() Function - Returns a new set object." + "\n" +
                        @"set(iterable)";
                case "setattr":
                    return "setattr() Function - Sets an attribute (property/method) of an object." + "\n" +
                        @"setattr(object, attribute, value)";
                case "slice":
                    return "slice() Function - Returns a slice object." + "\n" +
                        @"slice(start, end, step)";
                case "sorted":
                    return "sorted() Function - Returns a sorted list." + "\n" +
                        @"sorted(iterable, key=key, reverse=reverse)";
                case "staticmethod":
                    return "staticmethod() Function - Converts a method into a static method." + "\n" +
                        @"staticmethod(function) -> method";
                case "str":
                    return "str() Function - Returns a string object." + "\n" +
                        @"str(object, encoding=encoding, errors=errors)";
                case "sum":
                    return "sum() Function - Sums the items of an iterator." + "\n" +
                        @"sum(iterable, start)";
                case "super":
                    return "super() Function - Returns an object that represents the parent class." + "\n" +
                        @"super()";
                case "tuple":
                    return "tuple() Function - Returns a tuple." + "\n" +
                        @"tuple(iterable)";
                case "type":
                    return "type() Function - Returns the type of an object." + "\n" +
                        @"type(object, bases, dict)";
                case "vars":
                    return "vars() Function - Returns the __dict__ property of an object." + "\n" +
                        @"vars(object)";
                case "zip":
                    return "zip() Function - Returns an iterator, from two or more iterators." + "\n" +
                        @"zip(iterator1, iterator2, iterator3 ...)";
            }
            return null;
        }

        /// <summary>
        /// This class is responsible to set all the tooltips to built-in Python functions on the fly!
        /// Method reference: https://www.w3schools.com/python/python_ref_functions.asp
        /// </summary>
        public static void InitCalltips(object sender, CharAddedEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            int caretPos = TextArea.SelectionStart;
            string word = TextArea.GetWordFromPosition(caretPos - 1);
            TextArea.CallTipCancel();

            foreach (var Method in Methods.WordList)
            {
                if (e.Char == '(' && word.EndsWith(Method))
                {
                    string param = Methods.Hover(Method, TextArea);
                    TextArea.CallTipShow(TextArea.CurrentPosition, param);
                }
            }
        }
    }
}
