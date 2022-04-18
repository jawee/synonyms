import router from "next/router";
import { FC, useState } from "react";

interface IProp {
    word: string
}

const AddSynonymToWord: FC<IProp> = ({ word }) => {
    const [error, setError] = useState("");
    const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const target = e.target as typeof e.target & {
            synonym: { value: string };
        };

        const newSynonym = target.synonym.value;

        if (word === newSynonym) {
            setError("Synonym can't be the same as the word");
            return;
        }

        const body = {
            firstWord: word,
            secondWord: newSynonym
        }

        await fetch('/api/synonym', {
            method: 'POST',
            headers: {
                Accept: 'application.json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body),
            cache: 'default'
        }).then(() => {
            router.reload();
        });

    }
    return (
        <div className="mt-4">
            <h2 className="text-xl mb-3">Add a new synonym to <strong>{word}</strong></h2>
            <form className="w-full" onSubmit={onSubmit}>
                <input pattern="[a-zA-ZÅÄÖåäö]" required className="mr-3 bg-[#2F3E46] text-[#CAD2C5] shadow appearance-none border rounded py-2 px-3 mb-3 leading-tight focus:outline-none focus:shadow-outline" name="synonym" type="text" placeholder="New synonym" />
                <input className="border rounded py-2 px-3 mb-3 leading-tight hover:text-[#84A9BC]" type="submit" value="Add Synonym" />
            </form>
            <span className="text-[#e63946]">{error}</span>
        </div>
    )
}

export default AddSynonymToWord
