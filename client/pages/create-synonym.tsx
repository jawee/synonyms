import type { NextPage } from 'next'
import router from 'next/router';
import { useState } from 'react';

const CreateSynonym: NextPage = () => {
    const [error, setError] = useState("");
    const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const target = e.target as typeof e.target & {
            firstWord: { value: string },
            secondWord: { value: string },
        };

        if (target.firstWord.value === target.secondWord.value) {
            setError("The word and the synonym must be different");
            return;
        }

        const body = {
            firstWord: target.firstWord.value,
            secondWord: target.secondWord.value
        };

        fetch('/api/synonym', {
            method: 'POST',
            headers: {
                Accept: 'application.json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body),
            cache: 'default'
        }).then(() => {
            router.push(`/word/${target.firstWord.value}`);
        });
    }
    return (
        <div className="container">
            <form className="w-full" onSubmit={onSubmit}>
                <input pattern="[a-zA-ZÅÄÖåäö]" required className="mr-3 bg-[#2F3E46] text-[#CAD2C5] shadow appearance-none border rounded py-2 px-3 mb-3 leading-tight focus:outline-none focus:shadow-outline" name="firstWord" type="text" placeholder="Word" />
                <input pattern="[a-zA-ZÅÄÖåäö]" required className="mr-3 bg-[#2F3E46] text-[#CAD2C5] shadow appearance-none border rounded py-2 px-3 mb-3 leading-tight focus:outline-none focus:shadow-outline "name="secondWord" type="text" placeholder="Synonym" />
                <input className="border rounded py-2 px-3 mb-3 leading-tight hover:text-[#84A9BC]" type="submit" value="Add Synonym" />
            </form>
            <span className="text-[#e63946]">{error}</span>
        </div>
    )
}

export default CreateSynonym
