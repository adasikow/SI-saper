import math
import random
from pickle import ( dump, load, )

def loadNN(filename):
    with open(filename, 'rb') as my_file:
        return load(my_file)

def sigmoid(x):
    return math.tanh(x)

def dsigmoid(y):
    return 1.0 - y**2

def makeMatrix(Y, X, fill=0.0):
    m = []
    for i in range(Y):
        m.append([fill]*X)
    return m

def addMatrix(X, Y):
    return [[X[i][j] + Y[i][j]  for j in range(len(X[0]))] for i in range(len(X))]

def printMatrix(matrix):
    for i in matrix:
        print i

def normalizeMatrix(matrix):
    max = 0
    for i in matrix:
        for j in i:
            if j > max:
                max = float(j)
                #print max
    for i in range(len(matrix)):
        for j in range(len(matrix)):
            matrix[i][j] = round( matrix[i][j] / max, 2 )
    return matrix

def compareMatrix(A, B, mode): # mode zero  liczy ile min saper ie znalazl, mode jeden liczy ile falszywych min znalazl saper, mode dwa - wszystkie
    if len(A) != len(B): return -1
    n = len(A)
    differences = 0
    for i in range(0, n):
        for j in range(0, n):
            if mode ==0:
                if float(A[i][j])==1.0 and float(B[i][j])==0.0:
                    differences +=1
                    #print i, j
            elif mode ==1:
                if float(A[i][j])==0.0 and float(B[i][j])==1.0:
                    differences +=1
            elif mode ==2:
                if float(A[i][j]) != float(B[i][j]):
                    differences +=1
                    print i, j
    return differences

class NN:
    def __init__(self,numinput,numhidden,numoutput):

        self.numinput=numinput+1 #+1 for bias input node
        self.numhidden=numhidden
        self.numoutput=numoutput

        self.inputact=[1.0]*self.numinput
        self.hiddenact=[1.0]*self.numhidden
        self.outputact=[1.0]*self.numoutput

        self.inputweights=makeMatrix(self.numinput,self.numhidden)
        self.outpweights=makeMatrix(self.numhidden,self.numoutput)

        #randomize weights
        for i in range(self.numinput):
            for j in range(self.numhidden):
                self.inputweights[i][j] = random.uniform(-0.1, 0.1)
        for j in range(self.numhidden):
            for k in range(self.numoutput):
                self.outpweights[j][k] = random.uniform(-0.1, 0.1)

        self.inputchange = makeMatrix(self.numinput, self.numhidden)
        self.outputchange = makeMatrix(self.numhidden, self.numoutput)
        #TODO:Random fill matrix of weights

    def update(self,inputs):
        """Update network"""

        if len(inputs) != self.numinput-1:
            raise ValueError('Wrong number of inputs, should have %i inputs.' % self.numinput)

        #ACTIVATE ALL NEURONS INSIDE A NETWORK

        #Activate input layers neurons (-1 ignore bias node)
        for i in range(self.numinput-1):
            self.inputact[i] = inputs[i]

        #Activate hidden layers neurons
        for h in range(self.numhidden):
            sum = 0.0
            for i in range(self.numinput):
                sum = sum + self.inputact[i] * self.inputweights[i][h]
            self.hiddenact[h] = sigmoid(sum)

        #Activate output layers neurons
        for o in range(self.numoutput):
            sum = 0.0
            for h in range(self.numhidden):
                sum = sum + self.hiddenact[h] * self.outpweights[h][o]
            self.outputact[o] = sigmoid(sum)

        return self.outputact[:]

    def backPropagate(self, targets, learningrate, momentum):
        """Back Propagate """

        if len(targets) != self.numoutput:
            raise ValueError('Wrong number of target values.')

        # calculate error for output neurons
        output_deltas = [0.0] * self.numoutput
        for k in range(self.numoutput):
            error = targets[k]-self.outputact[k]
            output_deltas[k] = dsigmoid(self.outputact[k]) * error

        # calculate error for hidden neurons
        hidden_deltas = [0.0] * self.numhidden
        for j in range(self.numhidden):
            error = 0.0
            for k in range(self.numoutput):
                error = error + output_deltas[k]*self.outpweights[j][k]
            hidden_deltas[j] = dsigmoid(self.hiddenact[j]) * error

        # update output weights
        for j in range(self.numhidden):
            for k in range(self.numoutput):
                change = output_deltas[k]*self.hiddenact[j]
                self.outpweights[j][k] += learningrate*change + momentum*self.outputchange[j][k]
                self.outputchange[j][k] = change

        # update input weights
        for i in range(self.numinput):
            for j in range(self.numhidden):
                change = hidden_deltas[j]*self.inputact[i]
                self.inputweights[i][j] += learningrate*change + momentum*self.inputchange[i][j]
                self.inputchange[i][j] = change

        # calculate error
        error = 0.0
        for k in range(len(targets)):
            error = error + 0.5*(targets[k]-self.outputact[k])**2
        return error

    def train(self, patterns, iterations=1000, learningrate=0.5, momentum=0.1):
        for i in range(iterations):
            error = 0.0

            for p in patterns:
                inputs = p[0]
                targets = p[1]
                self.update(inputs)
                error = error + self.backPropagate(targets, learningrate, momentum)
            if i % 100 == 0:
                print('error %-.5f' % error)
        return error

def rebuildMap(patterns):
    mapsize = int(math.sqrt(len(patterns)))
    boolField = makeMatrix(mapsize, mapsize, 0)
    i = 0
    for j in range(0, mapsize):
        for k in range(0, mapsize):
                boolField[j][k] = int(patterns[i][1][0])
                i = i+1
    return boolField

def testVectors(patterns, networkfile):
    network = loadNN(networkfile)
    for pat in patterns:
        pat[1][0] = int(network.update(pat[0])[0]+0.5)
    return patterns

def test(inputfile, networkfile):
    network = loadNN(networkfile)
    patterns = loadPatterns(inputfile)
    correct = 0
    mines = 0
    expected = 0
    found = 0
    for pat in patterns:
        x = int(network.update(pat[0])[0]+0.5)
        if pat[1][0] == 1.0 : expected+=1
        if x==1.0: found+=1
        if pat[1][0] == float(x):
            correct+=1
            if float(x)==1.0:
                mines+=1
    print "Fields tested: "+str(len(patterns))+"\n correct: "+str(correct)+"\n mines found correctly: "+str(mines)+"\n mines expected: "+str(expected)+"\n total mines found: "+str(found)

def theOracle(my_matrix):
    mapsize = len(my_matrix)
    prophecy = makeMatrix(mapsize, mapsize, 0)
    for i in range(0, mapsize):
        for j in range(0, mapsize):
            if (my_matrix[i][j] >=0.1): # tutaj ustawia sie  wrazliwosc, tzn. ile razy na 100 sieci ma wykryc zeby uznac ze jest tam mina
                prophecy[i][j] = 1
    return prophecy

class Minefield(): # n to wielkosc mapy, zakladamy ze jest kwadratowa
    def __init__(self, n):
        self.radiationField = makeMatrix(n, n, 0.0)
        self.boolField = makeMatrix(n, n, 0)
        self.mapsize = n
    def loadRadiation(self, filename):
        my_file = open(filename, "r")
        my_string = my_file.read()
        my_list = my_string.split("\n")
        new_list = []
        for i in my_list:
            if i !="":
                newer_list = []
                worklist = i.split(" ")
                for j in worklist:
                    if j!="":
                        newer_list.append(float(j))
                new_list.append(newer_list)
        self.radiationField = new_list

    def addMine(self, x, y, value):
        self.radiationField[x][y] += value
        self.boolField[x][y] = 1
        self.radiationField[x-1][y] += value/2
        self.radiationField[x][y-1] += value/2
        self.radiationField[x+1][y] += value/2
        self.radiationField[x][y+1] += value/2
        self.radiationField[x-2][y] += value/4
        self.radiationField[x][y-2] += value/4
        self.radiationField[x+2][y] += value/4
        self.radiationField[x][y+2] += value/4
        self.radiationField[x-1][y-1] += value/4
        self.radiationField[x+1][y-1] += value/4
        self.radiationField[x+1][y+1] += value/4
        self.radiationField[x-1][y+1] += value/4
    def bombDresden(self, number):
        while(number>0):
            y = randint(2, self.mapsize-3)
            x = randint(2, self.mapsize-3)
            if (self.boolField[x][y]!=1):
                z = randint(0,2)
                value = float(z)*0.2+0.6
                self.addMine(x, y, value)
                number -=1
        self.radiationField = normalizeMatrix(self.radiationField)
    def getVector(self, x, y, mode): # ostatni parametr informuje o tym czy wektor jest do nauki czy do testow
        n = len(self.radiationField)
        my_vector = []
        if ((x>2)and(x<n-2) and (y>2)and(y<n-2)):
            my_vector.append(self.radiationField[x][y-2])
            my_vector.append(self.radiationField[x-1][y-1])
            my_vector.append(self.radiationField[x][y-1])
            my_vector.append(self.radiationField[x+1][y-1])
            my_vector.append(self.radiationField[x-2][y])
            my_vector.append(self.radiationField[x-1][y])
            my_vector.append(self.radiationField[x][y])
            my_vector.append(self.radiationField[x+1][y])
            my_vector.append(self.radiationField[x+2][y])
            my_vector.append(self.radiationField[x-1][y+1])
            my_vector.append(self.radiationField[x][y+1])
            my_vector.append(self.radiationField[x+1][y+1])
            my_vector.append(self.radiationField[x][y+2])
        else:
            for i in range(0, 13):
                my_vector.append(0.0)

        my_vector.append(1) # kontrolna jedynka na koncu
        if mode:
            my_answer = float((self.boolField[x][y])) # dane do porownania wyniku
        else:
            my_answer = [0] # w przypadku dla testow
        return [my_vector, my_answer]
    def getAllVectors(self, mode):
        vectors = []
        for i in range(0, self.mapsize):
            for j in range(0, self.mapsize):
                vectors.append(self.getVector(i, j, mode))
        return vectors

def saveBoolField(filename, boolfield):
        my_string = ""
        my_file = open(filename, "w")
        for i in boolfield:
            for j in i:
                my_string+=str(j)+" "
            my_string+="\n"
        my_file.write(my_string)

def loadBoolField(filename):
    my_file = open(filename, "r")
    my_string = my_file.read()
    my_list = my_string.split("\n")
    new_list = []
    for i in my_list:
        if i !="":
            newer_list = []
            worklist = i.split(" ")
            for j in worklist:
                if j!="":
                    newer_list.append(int(j))
            new_list.append(newer_list)
    return new_list

def saveCoordinates(boolfield, outputfile):
    my_string = ""
    mapsize = len(boolfield)
    for i in range(mapsize):
        for j in range(mapsize):
            if boolfield[i][j] == 1:
                my_string+=str(i)+" "+str(j)+"\n"
    with open(outputfile, "w") as my_file:
        my_file.write(my_string)

def main(filenames):
    inputfile = filenames[0]
    outputfile = filenames[1]

    my_field = Minefield(15)
    my_field.loadRadiation(inputfile)
    patterns = my_field.getAllVectors(0)

    boolfield =rebuildMap(testVectors(patterns, "network0"))
    for i in range(1, 100):
        boolfield = addMatrix(boolfield, rebuildMap(testVectors(patterns, "network"+str(i))))
    final = theOracle(normalizeMatrix(boolfield))
    saveBoolField(outputfile, final)
    #printMatrix(final)
    saveCoordinates(final, outputfile)

main(["input.txt", "output.txt"]) # jakby co to mozna sobie porownac zipa pomoca funkcji loadBoolField i compareMatrix
