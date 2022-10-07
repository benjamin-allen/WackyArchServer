import random
accumulator = 0

def generator(data_in, function):
    json_in = "["
    data_in_prep = data_in
    if isinstance(data_in[0], int):
        data_in_prep = [[x] for x in data_in]
    for i in range(len(data_in_prep[0])):
        din = [data_in_prep[j][i] for j in range(len(data_in))]
        pipe_in_json= "{\"Name\":\"" + str(i) + "\", \"Data\":" + str(din) + "}"
        json_in += pipe_in_json
        json_in += ", " if i != len(data_in_prep[0]) - 1 else ""
    json_in += "]"
    print(json_in)
    
    output = list(map(function, data_in))
    json_out = "["
    for i in range(len(output[0])):
        dout = [output[j][i] for j in range(len(output))]
        pipe_out_json = "{\"Name\":\"" + str(i) + "\", \"Data\":" + str(dout) + "}"
        json_out += pipe_out_json
        json_out += ", " if i != len(output[0]) - 1 else ""
    json_out += "]"

    print(json_out)

def create_test_inputs(input_count, input_list_size=1, upper_bound=2047, lower_bound=-2048):
    if input_list_size == 1:
        return [random.randint(lower_bound, upper_bound) for x in range(input_count)]
    else:
        return [[random.randint(lower_bound, upper_bound) for x in range(input_list_size)] for y in range(input_count)]

def hex_to_dec(hexStr):
    # Takes a string of hex values separated by white space: AA F3 00 2B
    d = hexStr.split()
    return list(map(lambda x: int(x, 16), d))

def bitwise_f(x): # challenge ID: 8BF70D54-17D8-454B-841A-48E42B7D4B21
    bits = sum(list(map(int, list(bin(x)[2:]))))
    return [12-bits, bits]

def tutorial_0(x):
    # For tutorial 0 I just want people to read and write output
    return [x]

def tutorial_1(x):
    # For tutorial 1 they should add the first two numbers, sub the next two, etc.
    return [x[0][0] + x[0][1], x[1][0] - x[1][1], x[2][0] * x[2][1], x[3][0]//x[3][1], x[4][0]%x[4][1]]

def tutorial_3(x):
    global accumulator
    accumulator = x+accumulator
    return [accumulator]

def comparitator(x):
    if (x[0] < x[1]):
        return [x[0], x[1]]
    elif (x[0] > x[1]):
        return [x[1], x[0]]
    else:
        return [2048,2048]

def signal_comp(x):
    return x.index(1)


def create_signal_comp_input(num_outputs):
    outputs = create_test_inputs(num_outputs, 1, 3, 0)
    inputs = []
    for o in outputs:
        steps = random.randint(0, 20)
        for s in range(steps):
            inputs.append([0,0,0,0])
        inputs.append([1 if i == o else 0 for i in range(4)])
    return (inputs, outputs)

def generate_bcd_values(num):
    outs = []
    for i in range(num):
        bcd_str = ""
        for j in range(3):
            bcd_int = random.randint(0,9)
            if j == 0 and bcd_int >= 8:
                bcd_int = 7
            bcd_str += str(bcd_int)
        v = int(bcd_str, 16)
        outs.append((v, bcd_str))
    return outs
