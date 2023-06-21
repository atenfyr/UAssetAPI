dat = ""
with open("index.md", "r") as f:
    dat = f.read()

res = dat.replace("\n\n", "\n").split("\n")[1:]
for i in range(len(res)):
    if len(res[i]) < 2: continue
    if res[i][:2] == "##":
        res[i] = "  - [" + res[i][3:] + "]()"
    else:
        res[i] = "    - " + res[i].replace("./", "api/")
    res[i] += "\n"

res_header = ""
with open("src/SUMMARY_header.md", "r") as f:
    res_header = f.read()
with open("src/SUMMARY.md", "w") as f:
    f.write(res_header)
    f.write("\n# Full Documentation\n- [Class Documentation]()\n")
    f.writelines(res)
