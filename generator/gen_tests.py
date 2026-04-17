import yaml
import os
from pathlib import Path

TEMPLATE = """// AUTO-GENERATED DO NOT EDIT
using System;
using NUnit.Framework;
using Lab.Interfaces;
using Lab.Implementations.GenCode1; 

namespace Module.Tests
{{
    [TestFixture]
    public class GeneratedTests
    {{
        private BinaryToDecimalConverter _sut = new BinaryToDecimalConverter();

        {methods}
    }}
}}"""

def generate():
    with open("config.yaml", 'r') as f:
        cfg = yaml.safe_load(f)
    with open(cfg['spec_path'], 'r') as f:
        spec = yaml.safe_load(f)

    test_methods = ""
    for m in spec['methods']:
        for eq in m['equivalence_classes']:
            inp = eq['inputs'][0]
            # Если вход - строка, добавляем кавычки
            val = f'"{inp}"' if isinstance(inp, str) else str(inp)
            
            # Формируем метод теста
            if eq['expected'] == "ArgumentException":
                action = f"Assert.Throws<ArgumentException>(() => _sut.{m['name']}({val}));"
            else:
                exp = f'"{eq["expected"]}"' if isinstance(eq['expected'], str) else str(eq['expected'])
                action = f"var res = _sut.{m['name']}({val});\n            Assert.That(res.ToString(), Is.EqualTo({exp}));"

            test_methods += f"""
        [Test]
        public void {m['name']}_{eq['case'].replace(" ", "_")}_Test()
        {{
            {action}
        }}\n"""

    full_code = TEMPLATE.format(methods=test_methods)
    out_dir = Path(cfg['output_dir'])
    out_dir.mkdir(parents=True, exist_ok=True)
    (out_dir / "GeneratedTests.cs").write_text(full_code)

if __name__ == "__main__":
    generate()
