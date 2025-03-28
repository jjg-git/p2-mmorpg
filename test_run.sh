#!/bin/bash
((testcase=1))
echo "Syntax: dotnet run instances tanks healers dpss minTime maxTime"
sleep 3s
echo ""
echo ""

echo "--Input validation--"
sleep 1s

echo "[$testcase]: All zeros"
sleep 1s
echo "dotnet run 0 0 0 0 0 0"
dotnet run 0 0 0 0 0 0
((testcase+=1))
echo ""

echo "[$testcase]: All zeros except minTime and maxTime"
sleep 1s
echo "dotnet run 0 0 0 0 1 2"
dotnet run 0 0 0 0 0 0
((testcase+=1))
echo ""

echo "[$testcase]: Negative 1s"
sleep 1s
echo "dotnet run -1 -1 -1 -1 -1 -1"
dotnet run 0 0 0 0 0 0
((testcase+=1))
echo ""

echo "[$testcase]: Negative Max value"
sleep 1s
echo "dotnet run -65535 -65535 -65535 -65535 -65535 -65535"
dotnet run -65535 -65535 -65535 -65535 -65535 -65535
((testcase+=1))
echo ""

echo "[$testcase]: Positive Max value"
sleep 1s
echo "dotnet run 65535 65535 65535 65535 0 15"
dotnet run 65535 65535 65535 65535 0 15
((testcase+=1))
echo ""

echo "[$testcase]: Non-equal time values"
sleep 1s
echo "dotnet run 0 0 0 0 1 2"
dotnet run 0 0 0 0 1 2
((testcase+=1))
echo ""

echo "[$testcase]: Equal time values"
sleep 1s
echo "dotnet run 0 0 0 0 2 2"
dotnet run 0 0 0 0 2 2
((testcase+=1))
echo ""


echo "--Program specifics--"
sleep 1s

echo "[$testcase]: One instance, no party"
sleep 1s
echo "dotnet run 1 0 0 0 1 2"
dotnet run 1 0 0 0 1 2
((testcase+=1))
echo ""

echo "[$testcase]: One party, no instance"
sleep 1s
echo "dotnet run 0 1 1 3 1 2"
dotnet run 0 1 1 3 1 2
((testcase+=1))
echo ""

echo "[$testcase]: One party, no instance with excess members"
sleep 1s
echo "dotnet run 0 1 1 6 1 2"
dotnet run 0 1 1 6 1 2
((testcase+=1))
echo ""

echo "[$testcase]: One party, one instance"
sleep 1s
echo "dotnet run 1 1 1 3 1 2"
dotnet run 1 1 1 3 1 2
((testcase+=1))
echo ""

echo "[$testcase]: One party, one instance, with excess members"
sleep 1s
echo "dotnet run 1 1 1 6 1 2"
dotnet run 1 1 1 6 1 2
((testcase+=1))
echo ""

echo "[$testcase]: Many instances, one party"
sleep 1s
echo "dotnet run 50 1 1 3 1 2"
dotnet run 50 1 1 3 1 2
((testcase+=1))
echo ""

echo "[$testcase]: Many parties, one instance"
sleep 1s
echo "dotnet run 1 50 50 150 1 2"
dotnet run 1 50 50 150 1 2
((testcase+=1))
echo ""

echo "[$testcase]: Many parties, one instance, with excess members"
sleep 1s
echo "dotnet run 1 50 60 200 1 2"
dotnet run 1 50 60 300 1 2
((testcase+=1))
echo ""

echo "[$testcase]: Many parties, many instances"
sleep 1s
echo "dotnet run 50 50 50 150 1 2"
dotnet run 50 50 50 150 1 2
((testcase+=1))
echo ""

echo "[$testcase]: Many parties, many instances, with excess members"
sleep 1s
echo "dotnet run 50 50 60 200 1 2"
dotnet run 50 50 60 300 1 2
((testcase+=1))
echo ""
