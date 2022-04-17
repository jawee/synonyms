import '../styles/globals.css'
import type { AppProps } from 'next/app'
import Layout from '../components/layout'

// https://coolors.co/palette/cad2c5-84a98c-52796f-354f52-2f3e46
function MyApp({ Component, pageProps }: AppProps) {
    return (
    <div className="h-screen bg-[#2F3E46] text-[#CAD2C5]">
        <Layout>
            <Component {...pageProps} />
        </Layout>
        </div>
    )
}

export default MyApp
